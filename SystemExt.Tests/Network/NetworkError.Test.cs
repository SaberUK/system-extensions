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
        /// Test the <see cref="NetworkError(NetworkOperation, Exception)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ExceptionConstructor()
        {
            const string socketMessage = "An attempt was made to access a socket in a way forbidden by its access permissions";
            var socketException = new SocketException((int)SocketError.AccessDenied);
            var socketError = new NetworkError(NetworkOperation.Read, socketException);

            // Ensure we get the correct response from Message and ToString with a top level socket exception.
            Assert.AreEqual(socketMessage, socketError.Message);
            Assert.AreEqual(string.Format("Read error ({0})", socketMessage), socketError.ToString());

            // Ensure we get the correct error code from Code and Operation with a top level socket exception.
            Assert.AreEqual(SocketError.AccessDenied, socketError.Code);
            Assert.AreEqual(NetworkOperation.Read, socketError.Operation);

            var ioException = new IOException("Testing!", socketException);
            var ioError = new NetworkError(NetworkOperation.Write, ioException);

            // Ensure we get the correct response from Message and ToString with an inner socket exception.
            Assert.AreEqual(socketMessage, ioError.Message);
            Assert.AreEqual(string.Format("Write error ({0})", socketMessage), ioError.ToString());

            // Ensure we get the correct error code from Code and Operation with an inner socket exception.
            Assert.AreEqual(SocketError.AccessDenied, ioError.Code);
            Assert.AreEqual(NetworkOperation.Write, ioError.Operation);

            const string argumentMessage = "Value does not fall within the expected range.";
            var argumentException = new ArgumentException();
            var argumentError = new NetworkError(NetworkOperation.Read, argumentException);

            // Ensure we get the correct response from Message and ToString with no socket exception.
            Assert.AreEqual(argumentMessage, argumentError.Message);
            Assert.AreEqual(string.Format("Read error ({0})", argumentMessage), argumentError.ToString());

            // Ensure we get the correct error code from Code and Operation with no socket exception.
            Assert.AreEqual(SocketError.SocketError, argumentError.Code);
            Assert.AreEqual(NetworkOperation.Read, argumentError.Operation);
        }

        /// <summary>
        /// Test the <see cref="NetworkError(NetworkOperation, string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void StringConstructor()
        {
            const string message = "Testing!";
            var error = new NetworkError(NetworkOperation.Write, message);

            // Ensure we get the correct response from Message and ToString.
            Assert.AreEqual(message, error.Message);
            Assert.AreEqual(string.Format("Write error ({0})", message), error.ToString());

            // Ensure we get the correct error code from Code and Operation.
            Assert.AreEqual(SocketError.SocketError, error.Code);
            Assert.AreEqual(NetworkOperation.Write, error.Operation);
        }
    }
}
