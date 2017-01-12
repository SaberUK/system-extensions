/**
 * System Extensions
 *
 *   Copyright (C) 2014-2017 Peter "SaberUK" Powell <petpow@saberuk.com>
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
 * in compliance with the License. You may obtain a copy of the License at
 *
 *   https://www.apache.org/licenses/LICENSE-2.0.html
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License
 * is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
 * or implied. See the License for the specific language governing permissions and limitations under
 * the License.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace SystemExt.Network
{

    /// <summary>
    /// Implements a wrapper around a TCP server.
    /// </summary>
    public sealed class TCPServer<TClient>
        where TClient : TCPServerClient<TClient>, new()
    {

        /// <summary>
        /// The endpoint on which the socket is listening.
        /// </summary>
        public IPEndPoint EndPoint
        {
            get { return this.Socket == null ? null : this.Socket.LocalEndPoint as IPEndPoint; }
        }

        /// <summary>
        /// Implementation defined data.
        /// </summary>
        public object Data;

        /// <summary>
        /// The clients which have connected to this server.
        /// </summary>
        internal readonly List<TClient> Clients;

        /// <summary>
        /// The underlying .NET socket.
        /// </summary>
        private Socket Socket;

        /// <summary>
        /// Initialize a new instance of the <see cref="TCPServer{TClient}"/> class with the
        /// specified address and port to listen on.
        /// </summary>
        /// <param name="address">
        /// IP address to listen on.
        /// </param>
        /// <param name="port">
        /// TCP port to listen on.
        /// </param>
        public TCPServer(IPAddress address, ushort port)
            : this(new IPEndPoint(address, port)) { }

        /// <summary>
        /// Initialize a new instance of the <see cref="TCPServer{TClient}"/> class with the
        /// specified end point to listen on.
        /// </summary>
        /// <param name="endPoint">
        /// IP address and TCP port to listen on.
        /// </param>
        public TCPServer(IPEndPoint endPoint)
        {
            this.Clients = new List<TClient>();
            this.Socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            this.Socket.Bind(endPoint);
            this.Socket.Listen(int.MaxValue);
            this.Socket.BeginAccept(this.EndAccept, null);
        }

        /// <summary>
        /// Retrieves a list of clients which are connected to this server.
        /// </summary>
        /// <returns>
        /// A list of clients which are connected to this server.
        /// </returns>
        public ReadOnlyCollection<TClient> GetClients()
        {
            return this.Clients.AsReadOnly();
        }

        /// <summary>
        /// Stop listening for connections.
        /// </summary>
        /// <param name="closeClients">
        /// Whether to also stop clients that are connected to this server.
        /// </param>
        public void Stop(bool closeClients = false)
        {
            // If the socket is already dead we don't need to do anything.
            if (this.Socket == null)
                return;

            // Terminate and then null the socket.
            this.Socket.Close();
            this.Socket = null;

            // Terminate all client connections.
            foreach (var client in this.Clients.ToArray())
            {
                if (closeClients)
                {
                    client.Close();
                    continue;
                }

                // Detach the child from the dead server.
                client.DetachServer();
            }
        }

        /// <summary>
        /// Callback for Socket#BeginAccept.
        /// </summary>
        /// <param name="result">
        /// The status of the asynchronous operation.
        /// </param>
        private void EndAccept(IAsyncResult result)
        {
            // Finish accepting the current client.
            var socket = Maybe<Socket>.FromThrowingFunc(() => this.Socket.EndAccept(result));

            // If the socket read failed then the socket is dead.
            if (!socket.HasValue)
                return;

            // Create a client object and add it to the child list.
            var client = new TClient();
            client.SetSocket(this, socket.Get());
            this.Clients.Add(client);

            // Accept the next client.
            this.Socket.BeginAccept(this.EndAccept, null);
        }
    }
}
