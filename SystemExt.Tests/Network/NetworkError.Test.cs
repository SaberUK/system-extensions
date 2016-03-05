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
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;

using SystemExt.Network;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Network
{

    /// <summary>
    /// Implements tests for the <see cref="NetworkError"/> class.
    /// </summary>
    [TestClass]
    public sealed class NetworkError_Test
    {

        /// <summary>
        /// Test the <see cref="NetworkError(Exception)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ExceptionConstructor()
        {
            const string socketMessage = "An attempt was made to access a socket in a way forbidden by its access permissions";
            var socketException = new SocketException((int)SocketError.AccessDenied);
            var socketError = new NetworkError(socketException);

            // Ensure we get the correct response from Message and ToString with a top level socket exception.
            Assert.AreEqual(socketMessage, socketError.Message);
            Assert.AreEqual(socketMessage, socketError.ToString());

            // Ensure we get the correct error code from Code with a top level socket exception.
            Assert.AreEqual(SocketError.AccessDenied, socketError.Code);

            var ioException = new IOException("Testing!", socketException);
            var ioError = new NetworkError(ioException);

            // Ensure we get the correct response from Message and ToString with an inner socket exception.
            Assert.AreEqual(socketMessage, ioError.Message);
            Assert.AreEqual(socketMessage, ioError.ToString());

            // Ensure we get the correct error code from Code with an inner socket exception.
            Assert.AreEqual(SocketError.AccessDenied, ioError.Code);

            const string argumentMessage = "Value does not fall within the expected range.";
            var argumentException = new ArgumentException();
            var argumentError = new NetworkError(argumentException);

            // Ensure we get the correct response from Message and ToString with no socket exception.
            Assert.AreEqual(argumentMessage, argumentError.Message);
            Assert.AreEqual(argumentMessage, argumentError.ToString());

            // Ensure we get the correct error code from Code with no socket exception.
            Assert.AreEqual(SocketError.SocketError, argumentError.Code);
        }

        /// <summary>
        /// Test the <see cref="NetworkError(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void StringConstructor()
        {
            const string message = "Testing!";
            var error = new NetworkError(message);

            // Ensure we get the correct response from Message and ToString.
            Assert.AreEqual(message, error.Message);
            Assert.AreEqual(message, error.ToString());

            // Ensure we get the correct error code from Code.
            Assert.AreEqual(SocketError.SocketError, error.Code);
        }
    }
}
