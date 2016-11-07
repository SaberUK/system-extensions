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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SystemExt.Network
{
    
    /// <summary>
    /// Base class for TCP clients.
    /// </summary>
    public abstract class TCPClientBase
    {

        /// <summary>
        /// The endpoint on which the socket is connected to.
        /// </summary>
        public IPEndPoint LocalEndPoint { get; private set; }

        /// <summary>
        /// The endpoint on which the socket is connecting from.
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// The buffer that bytes are stored in when read.
        /// </summary>
        internal protected readonly byte[] ReadBuffer;

        /// <summary>
        /// The underlying network stream.
        /// </summary>
        internal protected Stream Stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPClientBase"/> class.
        /// </summary>
        /// <param name="bufferSize">
        /// The size of the read buffer.
        /// </param>
        internal protected TCPClientBase(ushort bufferSize)
        {
            this.ReadBuffer = new byte[bufferSize];
        }

        /// <summary>
        /// Terminate the connection.
        /// </summary>
        public void Close()
        {
            this.CloseInternal(null);
        }

        /// <summary>
        /// Write a string to the client.
        /// </summary>
        /// <param name="data">
        /// The string to write to the client.
        /// </param>
        /// <param name="encoding">
        /// The encoding which the string should be decoded with.
        /// </param>
        /// <returns>
        /// True if starting the write succeeded, otherwise false.
        /// </returns>
        public bool Write(string data, Encoding encoding = null)
        {
            return this.Write(data, 0, data.Length, encoding);
        }

        /// <summary>
        /// Write a string to the client.
        /// </summary>
        /// <param name="data">
        /// The string to write to the client.
        /// </param>
        /// <param name="offset">
        /// The offset from the start of <see cref="data"/> to start writing.
        /// </param>
        /// <param name="count">
        /// The maximum number of characters to write.
        /// </param>
        /// <param name="encoding">
        /// The encoding which the string should be decoded with.
        /// </param>
        /// <returns>
        /// True if starting the write succeeded, otherwise false.
        /// </returns>
        public bool Write(string data, int offset, int count, Encoding encoding = null)
        {
            // Default to UTF-8 if an encoding is not specified.
            if (encoding == null)
                encoding = Encoding.UTF8;

            // Decode the string to a byte array and send it.
            var bytes = Maybe<byte[]>.FromThrowingFunc(() => encoding.GetBytes(data.Substring(offset, count)));
            return bytes.HasValue && this.Write(bytes.Get());
        }

        /// <summary>
        /// Write an array of bytes to the client.
        /// </summary>
        /// <param name="data">
        /// The data to write to the client.
        /// </param>
        /// <returns>
        /// True if starting the write succeeded, otherwise false.
        /// </returns>
        public bool Write(params byte[] data)
        {
            return this.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Write an array of bytes to the client.
        /// </summary>
        /// <param name="data">
        /// The data to write to the clienbt.
        /// </param>
        /// <param name="offset">
        /// The offset from the start of <see cref="data"/> to start writing.
        /// </param>
        /// <param name="count">
        /// The maximum number of bytes to write.
        /// </param>
        /// <returns>
        /// True if starting the write succeeded, otherwise false.
        /// </returns>
        public bool Write(byte[] data, int offset, int count)
        {
            // If the stream is dead we can't send anything.
            if (this.Stream == null)
                return false;

            try
            {
                this.Stream.BeginWrite(data, offset, count, this.EndWrite, new KeyValuePair<byte[], int>(data, count - offset));
            }
            catch (Exception exception)
            {
                // Close the socket.
                this.CloseInternal(new NetworkError(NetworkOperation.Write, exception));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Event which is called when the network connection is closed.
        /// </summary>
        /// <param name="error">
        /// Either an instance of the <see cref="NetworkError"/> class which represents the error
        /// which caused the connection to close, or null if the connection closed cleanly.
        /// </param>
        protected abstract void OnClose(NetworkError error);

        /// <summary>
        /// Event which is called when the network connection is opened.
        /// </summary>
        protected abstract void OnOpen();

        /// <summary>
        /// Event which is is called when data has been read from the socket.
        /// </summary>
        /// <param name="data">
        /// The data which was read.
        /// </param>
        /// <param name="count">
        /// The number of bytes which were read.
        /// </param>
        protected abstract void OnRead(byte[] data, int count);

        /// <summary>
        /// Event which is called when data has been written over the socket.
        /// </summary>
        /// <param name="data">
        /// The data which was written.
        /// </param>
        /// <param name="count">
        /// The number of bytes which were written.
        /// </param>
        protected virtual void OnWrite(byte[] data, int count) { }

        /// <summary>
        /// Terminate the connection.
        /// </summary>
        /// <param name="error">
        /// The error which caused the connection to close.
        /// </param>
        protected virtual void CloseInternal(NetworkError error)
        {
            // If the stream is already dead we don't need to do anything.
            if (this.Stream == null)
                return;

            // Fire the close event so the parent can clean up.
            this.OnClose(error);

            // Terminate and then null the stream.
            this.Stream.Close();
            this.Stream = null;
        }

        /// <summary>
        /// Assigns a socket to the client.
        /// </summary>
        /// <param name="client">
        /// The underlying .NET socket.
        /// </param>
        protected void SetSocket(Socket client)
        {
            this.LocalEndPoint = client.LocalEndPoint as IPEndPoint;
            this.RemoteEndPoint = client.RemoteEndPoint as IPEndPoint;
            this.Stream = new NetworkStream(client, true);

            // Fire the initialization event and start reading.
            this.OnOpen();
            this.Stream.BeginRead(this.ReadBuffer, 0, this.ReadBuffer.Length, this.EndRead, null);
        }

        /// <summary>
        /// Callback for Stream#BeginRead.
        /// </summary>
        /// <param name="result">
        /// The status of the asynchronous operation.
        /// </param>
        private void EndRead(IAsyncResult result)
        {
            try
            {
                // If EndRead returns zero then the connection is dead.
                var byteCount = this.Stream.EndRead(result);
                if (byteCount == 0)
                {
                    this.CloseInternal(new NetworkError(NetworkOperation.Read, "Connection closed"));
                    return;
                }

                // Fire the receive event.
                this.OnRead(this.ReadBuffer, byteCount);

                // Start reading the next packet.
                this.Stream.BeginRead(this.ReadBuffer, 0, this.ReadBuffer.Length, this.EndRead, null);
            }
            catch (Exception exception)
            {
                // Close the socket.
                this.CloseInternal(new NetworkError(NetworkOperation.Read, exception));
            }
        }

        /// <summary>
        /// Callback for Stream#BeginWrite.
        /// </summary>
        /// <param name="result">
        /// The status of the asynchronous operation.
        /// </param>
        private void EndWrite(IAsyncResult result)
        {
            try
            {
                // Finish writing and fire the send event.
                this.Stream.EndWrite(result);

                var bytes = (KeyValuePair<byte[], int>)result.AsyncState;
                this.OnWrite(bytes.Key, bytes.Value);
            }
            catch (Exception exception)
            {
                // Close the socket.
                this.CloseInternal(new NetworkError(NetworkOperation.Write, exception));
            }
        }
    }
}
