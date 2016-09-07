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

using System.Net;
using System.Net.Sockets;

namespace SystemExt.Network
{

    /// <summary>
    /// Implements helper methods for network use.
    /// </summary>
    public static class NetworkHelper
    {

        /// <summary>
        /// Determines whether <see cref="text"/> contains a valid IP address.
        /// </summary>
        /// <param name="text">
        /// The value to validate as an IPv4 address.
        /// </param>
        /// <returns>
        /// True if <paramref name="text"/> is a valid IP address. Otherwise, false.
        /// </returns>
        public static bool IsValidAddress(string text)
        {
            IPAddress address;
            return IPAddress.TryParse(text, out address);
        }

        /// <summary>
        /// Determines whether <paramref name="text"/> contains a valid IPv4 address.
        /// </summary>
        /// <param name="text">
        /// The value to validate as an IPv4 address.
        /// </param>
        /// <returns>
        /// True if <paramref name="text"/> is a valid IPv4 address. Otherwise, false.
        /// </returns>
        public static bool IsValidAddressV4(string text)
        {
            IPAddress address;
            if (!IPAddress.TryParse(text, out address))
                return false;

            return address.AddressFamily == AddressFamily.InterNetwork;
        }

        /// <summary>
        /// Determines whether <paramref name="text"/> contains a valid IPv6 address.
        /// </summary>
        /// <param name="text">
        /// The value to validate as an IPv4 address.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public static bool IsValidAddressV6(string text)
        {
            IPAddress address;
            if (!IPAddress.TryParse(text, out address))
                return false;

            return address.AddressFamily == AddressFamily.InterNetworkV6;
        }
    }
}
