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

namespace SystemExt.Network
{

    /// <summary>
    /// An enumeration of network operations.
    /// </summary>
    public enum NetworkOperation
    {
        /// <summary>
        /// The operation is connecting to a remote server.
        /// </summary>
        Connect,

        /// <summary>
        /// The operation is looking up a hostname.
        /// </summary>
        DNS,

        /// <summary>
        /// The operation is reading from a socket.
        /// </summary>
        Read,

        /// <summary>
        /// The operation is writing to a socket.
        /// </summary>
        Write,
    }
}
