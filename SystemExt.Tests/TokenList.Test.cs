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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests
{

    /// <summary>
    /// Implements tests for the <see cref="TokenList"/> class.
    /// </summary>
    [TestClass]
    public sealed class TokenList_Test
    {

        /// <summary>
        /// Test usage of the <see cref="TokenList"/> class in permissive mode.
        /// </summary>
        [TestMethod]
        public void Permissive()
        {
            // Ensure that a basic list is parsed correctly.
            var list = new TokenList(new[] { "*" });
            Assert.IsTrue(list.HasToken("1"));

            // Ensure that RemoveToken on an existing list works.
            list.RemoveToken("2");
            Assert.IsTrue(list.HasToken("1"));
            Assert.IsFalse(list.HasToken("2"));

            // Ensure that AddToken on an existing list works.
            list.AddToken("2");
            Assert.IsTrue(list.HasToken("1"));
            Assert.IsTrue(list.HasToken("2"));
        }

        /// <summary>
        /// Test usage of the <see cref="TokenList"/> class in restrictive mode.
        /// </summary>
        [TestMethod]
        public void Restrictive()
        {
            // Ensure that a basic list is parsed correctly.
            var list = new TokenList(new[] { "1" });
            Assert.IsTrue(list.HasToken("1"));
            Assert.IsFalse(list.HasToken("2"));

            // Ensure that AddToken on an existing list works.
            list.AddToken("2");
            Assert.IsTrue(list.HasToken("1"));
            Assert.IsTrue(list.HasToken("2"));

            // Ensure that RemoveToken on an existing list works.
            list.RemoveToken("2");
            Assert.IsTrue(list.HasToken("1"));
            Assert.IsFalse(list.HasToken("2"));
        }
    }
}
