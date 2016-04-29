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

using SystemExt.Network;
using SystemExt.Terminal;

namespace SystemExt.Demo
{

    /// <summary>
    /// Demo for <see cref="SystemExt.Network"/>.
    /// </summary>
    public static class Network
    {

        private class EchoClient : TCPClient<EchoClient>
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="EchoClient"/> class.
            /// </summary>
            public EchoClient() : base(100) { }

            /// <summary>
            /// Event which is called when the network connection is closed.
            /// </summary>
            /// <param name="error">
            /// Either an instance of the <see cref="NetworkError"/> class which represents the error
            /// which caused the connection to close, or null if the connection closed cleanly.
            /// </param>
            protected override void OnClose(NetworkError error)
            {
                Console.WriteLine("Disconnected from {0} (error: {1})", this.RemoteEndPoint, error == null ? "none" : error.Message);
            }

            /// <summary>
            /// Event which is called when the network connection is opened.
            /// </summary>
            protected override void OnOpen()
            {
                Console.WriteLine("Connected to {0}", this.RemoteEndPoint);
            }

            /// <summary>
            /// Event which is is called when data has been read from the socket.
            /// </summary>
            /// <param name="data">
            /// The data which was read.
            /// </param>
            /// <param name="count">
            /// The number of bytes which were read.
            /// </param>
            protected override void OnRead(byte[] data, int count)
            {
                Console.WriteLine("Read: {0}", BitConverter.ToString(data, 0, count));
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
            protected override void OnWrite(byte[] data, int count)
            {
                Console.WriteLine("Written: {0}", BitConverter.ToString(data, 0, count));
            }
        }

        /// <summary>
        /// Implements a basic echo server.
        /// </summary>
        private class EchoServerClient : TCPServerClient<EchoServerClient>
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="EchoServerClient"/> class.
            /// </summary>
            public EchoServerClient()
                : base(100) { }

            /// <summary>
            /// Event which is called when the network connection is closed.
            /// </summary>
            /// <param name="error">
            /// Either an instance of the <see cref="NetworkError"/> class which represents the error
            /// which caused the connection to close, or null if the connection closed cleanly.
            /// </param>
            protected override void OnClose(NetworkError error)
            {
                Console.WriteLine("Client {0} ({1}) disconnected from {2} (error: {3})", this.GetHashCode(), this.RemoteEndPoint, this.Server.EndPoint, error == null ? "none" : error.Message);
            }

            /// <summary>
            /// Event which is called when the network connection is opened.
            /// </summary>
            protected override void OnOpen()
            {
                Console.WriteLine("Client {0} ({1}) connected to {2}", this.GetHashCode(), this.RemoteEndPoint, this.Server.EndPoint);
            }

            /// <summary>
            /// Event which is is called when data has been read from the socket.
            /// </summary>
            /// <param name="data">
            /// The data which was read.
            /// </param>
            /// <param name="count">
            /// The number of bytes which were read.
            /// </param>
            protected override void OnRead(byte[] data, int count)
            {
                Console.WriteLine("Read from {0}: {1}", this.GetHashCode(), BitConverter.ToString(data, 0, count));
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
            protected override void OnWrite(byte[] data, int count)
            {
                Console.WriteLine("Written to {0}: {1}", this.GetHashCode(), BitConverter.ToString(data, 0, count));
            }
        }

        /// <summary>
        /// Entry point for the <see cref="SystemExt.Log"/> demo.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        public static int EntryPoint(string[] args)
        {
            return new ApplicationChooser()
                .AddEntryPoint(RunEchoClient, "Start a TCP echo client")
                .AddEntryPoint(RunEchoServer, "Start a TCP echo server")
                .Run(args);
        }

        /// <summary>
        /// A demo which implements a basic TCP echo server.
        /// </summary>
        /// <param name="arg">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int RunEchoClient(string[] arg)
        {
            var address = Prompt.String("What adddress do you want to connect to", "127.0.0.1", NetworkHelper.IsValidAddress);
            var port = (ushort)Prompt.Integer("What port do you want to connect to", 9999, ushort.MinValue, ushort.MaxValue);
            var endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            var client = TCPClient<EchoClient>.Connect(endPoint);
            Console.WriteLine("Now connecting to {0}, press return to exit.", endPoint);
            for (var line = Console.ReadLine(); !string.IsNullOrEmpty(line); line = Console.ReadLine())
            {
                client.Write(line);
            }
            return 0;
        }

        /// <summary>
        /// A demo which implements a basic TCP echo server.
        /// </summary>
        /// <param name="arg">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int RunEchoServer(string[] arg)
        {
            var port = (ushort)Prompt.Integer("What port do you want to listen on", 9999, ushort.MinValue, ushort.MaxValue);
            var server = new TCPServer<EchoServerClient>(IPAddress.Any, port);
            Console.WriteLine("Now listening on {0}, press any key to exit.", server.EndPoint);
            Console.ReadKey();
            server.Stop(true);
            return 0;
        }
    }
}
