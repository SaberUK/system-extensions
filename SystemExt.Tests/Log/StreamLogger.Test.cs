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

using System.IO;

using SystemExt.Log;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Log
{

    /// <summary>
    /// Implements tests for the <see cref="StreamLogger"/> class.
    /// </summary>
    [TestClass]
    public sealed class StreamLogger_Test
    {

        /// <summary>
        /// Test the <see cref="StreamLogger.Write"/> method.
        /// </summary>
        [TestMethod]
        public void Write()
        {
            // Set up the listener for the debug window.
            var stream = new MemoryStream();
            var reader = new StreamReader(stream);

            // Create a debug logger and write a test message.
            var logger = new StreamLogger(stream);
            logger.Write("TEST", "This is a test");

            // Check that we get the correct output.
            stream.Position = 0;
            Assert.AreEqual("[TEST] This is a test", reader.ReadLine());
        }
    }
}
