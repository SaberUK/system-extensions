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

using System;

namespace SystemExt.Extensions
{

    /// <summary>
    /// Extension methods for the <see cref="DateTime"/> struct.
    /// </summary>
    public static class System_DateTime
    {

        /// <summary>
        /// Converts a <see cref="DateTime"/> to an <see cref="Int64"/> which represents the number
        /// of seconds since the UNIX epoch.
        /// </summary>
        /// <param name="self">
        /// An instance of the <see cref="DateTime"/> class.
        /// </param>
        /// <returns>
        /// The number of seconds between the UNIX epoch and the specified <see cref="DateTime"/>.
        /// </returns>
        public static long ToUnixTime(this DateTime self)
        {
            var timeSpan = self.Subtract(Constants.UnixEpoch);
            return (long)timeSpan.TotalSeconds;
        }
    }
}
