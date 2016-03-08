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
using System.Net.Sockets;

namespace SystemExt.Network
{

    /// <summary>
    /// Represents a network error.
    /// </summary>
    public sealed class NetworkError
    {

        /// <summary>
        /// An error code which represents the network error.
        /// </summary>
        public readonly SocketError Code;

        /// <summary>
        /// A human readable message which describes the network error.
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// The operation which was happening when this error occurred.
        /// </summary>
        public readonly NetworkOperation Operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkError"/> class with the specified
        /// socket error.
        /// </summary>
        /// <param name="operation">
        /// The operation which was happening when this error occurred.
        /// </param>
        /// <param name="message">
        /// A human readable message which describes the network error.
        /// </param>
        internal NetworkError(NetworkOperation operation, string message)
        {
            this.Code = SocketError.SocketError;
            this.Message = message;
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkError"/> class with the specified
        /// exception.
        /// </summary>
        /// <param name="operation">
        /// The operation which was happening when this error occurred.
        /// </param>
        /// <param name="exception">
        /// An exception which represents a network error.
        /// </param>
        internal NetworkError(NetworkOperation operation, Exception exception)
        {
            this.Operation = operation;
            for (var inner = exception; inner != null; inner = inner.InnerException)
            {
                if (!(inner is SocketException))
                    continue;

                // We have found a socket error.
                var socketException = (inner as SocketException);
                this.Code = socketException.SocketErrorCode;
                this.Message = socketException.Message.Replace(Environment.NewLine, " ");
                return;
            }

            // We haven't found a socket exception so use the exception message.
            this.Code = SocketError.SocketError;
            this.Message = exception.Message.Replace(Environment.NewLine, " ");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} error ({1})", this.Operation, this.Message);
        }
    }
}
