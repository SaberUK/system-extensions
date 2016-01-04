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
using System.Text;

namespace SystemExt.Log
{

    /// <summary>
    /// Implements a logger which writes to a stream.
    /// </summary>
    public sealed class StreamLogger : ILogger
    {

        /// <summary>
        /// The underlying stream writer.
        /// </summary>
        private readonly StreamWriter StreamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger"/> class with the specified
        /// stream to write to and optionally the encoding to use when writing.
        /// </summary>
        /// <param name="stream">
        /// The stream to write log messages to.
        /// </param>
        /// <param name="encoding">
        /// The encoding to use when writing to the stream.
        /// </param>
        public StreamLogger(Stream stream, Encoding encoding = null)
        {
            this.StreamWriter = new StreamWriter(stream, encoding ?? Encoding.Default)
            {
                AutoFlush = true
            };
        }

        /// <summary>
        /// Write a message to this logger.
        /// </summary>
        /// <param name="component">
        /// The component which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        public void Write(string component, string message)
        {
            this.StreamWriter.WriteLine("[{0}] {1}", component, message);
        }
    }
}
