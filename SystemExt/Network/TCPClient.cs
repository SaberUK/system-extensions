/**
 * System Extensions
 *
 *   Copyright (C) 2014-2016 Peter "SaberUK" Powell <petpow@saberuk.com>
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
using System.Net;
using System.Net.Sockets;

namespace SystemExt.Network
{

    /// <summary>
    /// Implements a wrapper around a TCP client.
    /// </summary>
    public abstract class TCPClient<TClient> : TCPClientBase
        where TClient : TCPClient<TClient>, new()
    {

        /// <summary>
        /// Implementation defined data.
        /// </summary>
        public object Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPServerClient{TClient}"/> class.
        /// </summary>
        /// <param name="bufferSize">
        /// The size of the read buffer.
        /// </param>
        protected TCPClient(ushort bufferSize) : base(bufferSize) { }

        /// <summary>
        /// Attempt to connect to the specified remote server.
        /// </summary>
        /// <param name="host">
        /// Hostname or IP address to connect to.
        /// </param>
        /// <param name="port">
        /// TCP port to connect to.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="TClient"/> class which represents the connection.
        /// </returns>
        public static TClient Connect(string host, ushort port)
        {
            IPAddress address;
            if (IPAddress.TryParse(host, out address))
                return TCPClient<TClient>.Connect(new IPEndPoint(address, port));

            var client = new TClient();
            Dns.BeginGetHostAddresses(host, client.EndGetHostAddresses, port);
            return client;
        }

        /// <summary>
        /// Attempt to connect to the specified remote server.
        /// </summary>
        /// <param name="address">
        /// IP address to connect to.
        /// </param>
        /// <param name="port">
        /// TCP port to connect to.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="TClient"/> class which represents the connection.
        /// </returns>
        public static TClient Connect(IPAddress address, ushort port)
        {
            return TCPClient<TClient>.Connect(new IPEndPoint(address, port));
        }

        /// <summary>
        /// Attempt to connect to the specified remote server.
        /// </summary>
        /// <param name="endPoint">
        /// IP address and TCP port to connect to.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="TClient"/> class which represents the connection.
        /// </returns>
        public static TClient Connect(IPEndPoint endPoint)
        {
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            var client = new TClient();

            socket.BeginConnect(endPoint, client.EndConnect, socket);
            return client;
        }

        /// <summary>
        /// Callback for Socket#BeginConnect.
        /// </summary>
        /// <param name="result">
        /// The status of the asynchronous operation.
        /// </param>
        private void EndConnect(IAsyncResult result)
        {
            try
            {
                var socket = result.AsyncState as Socket;
                socket.EndConnect(result);
                this.SetSocket(socket);
            }
            catch (Exception exception)
            {
                // Fire the close event.
                this.OnClose(new NetworkError(NetworkOperation.Connect, exception));
            }
        }

        /// <summary>
        /// Callback for Dns#BeginGetHostAddresses.
        /// </summary>
        /// <param name="result">
        /// The status of the asynchronous operation.
        /// </param>
        private void EndGetHostAddresses(IAsyncResult result)
        {
            try
            {
                var addresses = Dns.EndGetHostAddresses(result);
                var port = (ushort)result.AsyncState;
                if (addresses.Length == 0)
                {
                    this.OnClose(new NetworkError(NetworkOperation.DNS, "DNS lookup succeeded but returned no results"));
                    return;
                }

                var random = new Random();
                var address = addresses[random.Next(0, addresses.Length)];

                var socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(address, port, this.EndConnect, socket);
            }
            catch (Exception exception)
            {
                // Fire the close event.
                this.OnClose(new NetworkError(NetworkOperation.DNS, exception));
            }
        }
    }
}
