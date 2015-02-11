﻿/**
 * System Extensions
 *
 *   Copyright (C) 2014 Peter "SaberUK" Powell <petpow@saberuk.com>
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

using SystemExt.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Extensions
{

    /// <summary>
    /// Implements tests for the <see cref="System_Int64"/> class.
    /// </summary>
    [TestClass]
    public sealed class System_Int64_Test
    {

        /// <summary>
        /// Test the <see cref="System_Int64.FromUnixTime"/> extension method.
        /// </summary>
        [TestMethod]
        public void FromUnixTime()
        {
            // Ensure that we can handle negative UNIX timestamps.
            Assert.AreEqual(Constants.TuringDateTime, Constants.TuringUnixTime.FromUnixTime());

            // Ensure that we can handle positive UNIX timestamps.
            Assert.AreEqual(Constants.CenturyDateTime, Constants.CenturyUnixTime.FromUnixTime());
        }
    }
}