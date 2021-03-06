﻿/**
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

using SystemExt.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests.Extensions
{

    /// <summary>
    /// Implements tests for the <see cref="System_IO_Stream"/> class.
    /// </summary>
    [TestClass]
    public sealed class System_IO_Stream_Test
    {

        /// <summary>
        /// Test the <see cref="System_IO_Stream.HasReachedEnd"/> extension method.
        /// </summary>
        [TestMethod]
        public void HasReachedEnd()
        {
            // Data source.
            var stream = new MemoryStream();

            // Ensure that true is returned when we are at the end.
            Assert.IsTrue(stream.HasReachedEnd());

            // Write some data to the stream and move to the start.
            stream.WriteByte(42);
            stream.Position = 0;

            // Ensure that false is returned when we are not at the end.
            Assert.IsFalse(stream.HasReachedEnd());
        }
    }
}
