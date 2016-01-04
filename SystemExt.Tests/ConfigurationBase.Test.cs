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

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests
{

    /// <summary>
    /// Implements tests for the <see cref="ConfigurationBase{T}"/> class.
    /// </summary>
    [TestClass]
    public sealed class ConfigurationBase_Test
    {

        /// <summary>
        /// A configuration class that we use for testing.
        /// </summary>
        public sealed class TestConfiguration : ConfigurationBase<TestConfiguration>
        {

            /// <summary>
            /// Whether we should foo.
            /// </summary>
            public bool Foo { get; set; }

            /// <summary>
            /// How much we should bar.
            /// </summary>
            public int Bar { get; set; }

            /// <summary>
            /// The name of what we should baz.
            /// </summary>
            public string Baz { get; set; }

        }

        /// <summary>
        /// The file which contains test data.
        /// </summary>
        private const string TestFile = "TestData/ConfigurationBase.xml";

        /// <summary>
        /// Test the <see cref="ConfigurationBase{T}.Load"/> method.
        /// </summary>
        [DeploymentItem(ConfigurationBase_Test.TestFile)]
        [TestMethod]
        public void Load()
        {
            // Ensure we can read data correctly from a file.
            var configuration = TestConfiguration.Load(ConfigurationBase_Test.TestFile);
            Assert.IsFalse(configuration.Foo);
            Assert.AreEqual(-1, configuration.Bar);
            Assert.AreEqual("Nothing", configuration.Baz);
        }

        /// <summary>
        /// Test the <see cref="ConfigurationBase{T}.Save"/> method.
        /// </summary>
        [DeploymentItem(ConfigurationBase_Test.TestFile)]
        [TestMethod]
        public void Save()
        {
            // Create a dummy configuration and write it to a file.
            var configuration = new TestConfiguration
            {
                Foo = false,
                Bar = -1,
                Baz = "Nothing"
            };
            var location = Path.GetTempFileName();
            configuration.Save(location);

            var expected = File.ReadAllText(ConfigurationBase_Test.TestFile);
            var actual = File.ReadAllText(location);
            Assert.AreEqual(expected, actual);
        }
    }
}
