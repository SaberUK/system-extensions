/**
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

using System.Collections.Generic;

using SystemExt.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Extensions
{

    /// <summary>
    /// Implements tests for the <see cref="System_Collections_Generic_IList_T"/> class.
    /// </summary>
    [TestClass]
    public sealed class System_Collections_Generic_IList_T_Test
    {

        /// <summary>
        /// Test the <see cref="System_Collections_Generic_IList_T.AddRange{T}"/> extension method.
        /// </summary>
        [TestMethod]
        public void AddRange()
        {
            var testDataParent = new List<int>
            {
                1, 2, 3, 4, 5
            };
            var testDataChild = new List<int>
            {
                6, 7, 8, 9, 10
            };

            testDataParent.AddRange(testDataChild);

            // Ensure the parent list has the right amount of elements.
            Assert.AreEqual(testDataParent.Count, 10);

            // Ensure all of the child elements are in the parent.
            foreach (var element in testDataChild)
                Assert.IsTrue(testDataParent.Contains(element));
        }

        /// <summary>
        /// Test the <see cref="System_Collections_Generic_IList_T.ElementAtOrValue{T}"/> extension method.
        /// </summary>
        [TestMethod]
        public void ElementAtOrValue()
        {
            var testData = new List<int>
            {
                1, 2, 3, 4, 6
            };

            // Ensure that indexes in range return the correct values
            for (var i = 0; i < testData.Count; i++)
                Assert.AreEqual(testData[i], testData.ElementAtOrValue(i, -1));

            // Ensure that indexes which are out of range return the default value.
            Assert.AreEqual(testData.ElementAtOrValue(int.MinValue, -1), -1);
            Assert.AreEqual(testData.ElementAtOrValue(int.MaxValue, -1), -1);
        }
    }
}
