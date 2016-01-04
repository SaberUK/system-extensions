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
using System.Diagnostics;

using SystemExt.Log;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Log
{

    /// <summary>
    /// Implements tests for the <see cref="DebugLogger"/> class.
    /// </summary>
    [TestClass]
    public sealed class DebugLogger_Test
    {

        /// <summary>
        /// A trace listener which we use for testing.
        /// </summary>
        private sealed class TestTraceListener : TraceListener
        {

            /// <summary>
            /// [UNUSED] Callback which is called when a partial message is written to the debug console.
            /// </summary>
            /// <param name="message">
            /// A partial message which was written to the debug console.
            /// </param>
            public override void Write(string message)
            {
                // This should not be called by the logging system.
                throw new NotImplementedException();
            }

            /// <summary>
            /// Callback which is called when a message is written to the debug console.
            /// </summary>
            /// <param name="message">
            /// A message which was written to the debug console.
            /// </param>
            public override void WriteLine(string message)
            {
                Assert.AreEqual("[TEST] This is a test", message);
            }
        }

        /// <summary>
        /// Test the <see cref="DebugLogger.Write"/> method.
        /// </summary>
        [TestMethod]
        public void Write()
        {
            // Set up the listener for the debug window.
            Debug.Listeners.Add(new TestTraceListener());

            // Create a debug logger and write a test message.
            var logger = new DebugLogger();
            logger.Write("TEST", "This is a test");
        }
    }
}
