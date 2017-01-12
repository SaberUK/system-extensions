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

using SystemExt.Network;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Network
{

    /// <summary>
    /// Implements tests for the <see cref="NetworkHelper"/> class.
    /// </summary>
    [TestClass]
    public sealed class NetworkHelper_Test
    {

        /// <summary>
        /// Test the <see cref="NetworkHelper.IsValidAddress"/> method.
        /// </summary>
        [TestMethod]
        public void IsValidAddress()
        {
            // Ensure that a valid IPv4 results in true being returned.
            Assert.IsTrue(NetworkHelper.IsValidAddress("127.0.0.1"));

            // Ensure that a valid IPv6 results in true being returned.
            Assert.IsTrue(NetworkHelper.IsValidAddress("::1"));

            // Ensure that invalid data results in false being returned.
            Assert.IsFalse(NetworkHelper.IsValidAddress("192.168.1."));
            Assert.IsFalse(NetworkHelper.IsValidAddress("example.com"));
            Assert.IsFalse(NetworkHelper.IsValidAddress("invalid address"));
        }

        /// <summary>
        /// Test the <see cref="NetworkHelper.IsValidAddressV4"/> method.
        /// </summary>
        [TestMethod]
        public void IsValidAddressV4()
        {
            // Ensure that a valid IPv4 results in true being returned.
            Assert.IsTrue(NetworkHelper.IsValidAddressV4("127.0.0.1"));

            // Ensure that a valid IPv6 results in false being returned.
            Assert.IsFalse(NetworkHelper.IsValidAddressV4("::1"));

            // Ensure that invalid data results in false being returned.
            Assert.IsFalse(NetworkHelper.IsValidAddressV4("192.168.1."));
            Assert.IsFalse(NetworkHelper.IsValidAddressV4("example.com"));
            Assert.IsFalse(NetworkHelper.IsValidAddressV4("invalid address"));
        }

        /// <summary>
        /// Test the <see cref="NetworkHelper.IsValidAddressV6"/> method.
        /// </summary>
        [TestMethod]
        public void IsValidAddressV6()
        {
            // Ensure that a valid IPv6 results in true being returned.
            Assert.IsTrue(NetworkHelper.IsValidAddressV6("::1"));

            // Ensure that a valid IPv4 results in false being returned.
            Assert.IsFalse(NetworkHelper.IsValidAddressV6("127.0.0.1"));

            // Ensure that invalid data results in false being returned.
            Assert.IsFalse(NetworkHelper.IsValidAddressV6("192.168.1."));
            Assert.IsFalse(NetworkHelper.IsValidAddressV6("example.com"));
            Assert.IsFalse(NetworkHelper.IsValidAddressV6("invalid address"));
        }
    }
}
