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

using System.Text;

namespace SystemExt.Extensions
{

    /// <summary>
    /// Extension methods for the <see cref="string"/> class.
    /// </summary>
    public static class System_String
    {

        /// <summary>
        /// Create a new <see cref="string"/> which is <paramref name="self"/> repeated
        /// <paramref name="times"/> times.
        /// </summary>
        /// <param name="self">
        /// An instance of the <see cref="string"/> class.
        /// </param>
        /// <param name="times">
        /// The number of times to repeat <see cref="times"/>.
        /// </param>
        /// <returns>
        /// <paramref name="self"/> repeated <paramref name="times"/> times.
        /// </returns>
        public static string Repeat(this string self, uint times)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < times; i++)
                builder.Append(self);

            return builder.ToString();
        }
    }
}
