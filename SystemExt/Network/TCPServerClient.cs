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

using System.Net.Sockets;

namespace SystemExt.Network
{

    /// <summary>
    /// Implements a wrapper around the client of a TCP server.
    /// </summary>
    public abstract class TCPServerClient<TClient> : TCPClientBase
        where TClient : TCPServerClient<TClient>, new()
    {

        /// <summary>
        /// The <see cref="TCPServer{TClient}"/> which created this instance.
        /// </summary>
        protected TCPServer<TClient> Server { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPServerClient{TClient}"/> class.
        /// </summary>
        /// <param name="bufferSize">
        /// The size of the read buffer.
        /// </param>
        protected TCPServerClient(ushort bufferSize)
            : base(bufferSize) { }

        /// <summary>
        /// Detaches this client from its parent.
        /// </summary>
        internal void DetachServer()
        {
            this.Server = null;
        }

        /// <summary>
        /// Assigns a socket to the client.
        /// </summary>
        /// <param name="server">
        /// The <see cref="TCPServer{TClient}"/> which created this instance.
        /// </param>
        /// <param name="client">
        /// The underlying .NET socket.
        /// </param>
        internal void SetSocket(TCPServer<TClient> server, Socket client)
        {
            // Don't overwrite an initialized socket.
            if (this.Server != null || this.Stream != null)
                return;

            this.Server = server;
            this.SetSocket(client);
        }

        /// <summary>
        /// Terminate the connection.
        /// </summary>
        /// <param name="error">
        /// The error which caused the connection to close.
        /// </param>
        protected override void CloseInternal(NetworkError error)
        {
            // If the stream is already dead we don't need to do anything.
            if (this.Stream == null)
                return;

            // Shut down the stream.
            base.CloseInternal(error);

            if (this.Server == null)
                return;

            // Remove this instance from the client list.
            this.Server.Clients.Remove(this as TClient);
            this.DetachServer();
        }

        /// <summary>
        /// Event which is called when data has been written over the socket.
        /// </summary>
        /// <param name="data">
        /// The data which was written.
        /// </param>
        /// <param name="count">
        /// The number of bytes which were written.
        /// </param>
        protected override void OnWrite(byte[] data, int count) { }
    }
}
