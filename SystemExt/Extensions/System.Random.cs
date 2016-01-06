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
using System.Text;

namespace SystemExt.Extensions
{

    /// <summary>
    /// Extension methods for the <see cref="Random"/> class.
    /// </summary>
    public static class System_Random
    {

        /// <summary>
        /// Generates a random ASCII string with the specified length.
        /// </summary>
        /// <param name="random">
        /// An instance of the <see cref="Random"/> class.
        /// </param>
        /// <param name="length">
        /// The length of the string to generate.
        /// </param>
        /// <param name="printable">
        /// Whether to only use characters which are printable.
        /// </param>
        /// <returns>
        /// A new string containing <paramref name="length"/> random characters.
        /// </returns>
        public static string NextString(this Random random, byte length, bool printable = true)
        {
            var buffer = new byte[length];
            for (var pos = 0; pos < length; pos++)
            {
                buffer[pos] = (byte)(printable ? random.Next(33, 126) : random.Next(0, 255));
            }
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
