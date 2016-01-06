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
using System.Linq;

using SystemExt.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Extensions
{

    /// <summary>
    /// Implements tests for the <see cref="System_Random"/> class.
    /// </summary>
    [TestClass]
    public sealed class System_Random_Test
    {

        /// <summary>
        /// Test the <see cref="System_Random.NextString"/> extension method.
        /// </summary>
        [TestMethod]
        public void Test_System_Random_NextString()
        {
            // Data source.
            var random = new Random();

            // Ensure we get ten characters.
            var tenCharacters = random.NextString(10);
            Assert.AreEqual(10, tenCharacters.Length);

            // Ensure we get a string which contains only printable characters.
            var printableCharacters = random.NextString(byte.MaxValue);
            Assert.IsTrue(printableCharacters.Select(c => c < 33 || c > 126).Any());

            // Ensure we get a string which contains non-printable characters.
            Assert.IsTrue(Enumerable.Range(1, 1000).Any(i => random.NextString(byte.MaxValue).Select(c => c < 33 || c > 126).Any()));
        }
    }
}
