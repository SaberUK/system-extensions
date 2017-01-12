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

using System;

namespace SystemExt.Tests
{

    /// <summary>
    /// Constants which are used by multiple unit tests.
    /// </summary>
    internal static class Constants
    {

        /// <summary>
        /// The start of the 21st century CE as a <see cref="DateTime"/>.
        /// </summary>
        internal static readonly DateTime CenturyDateTime = new DateTime(2001, 1, 1);

        /// <summary>
        /// The start of the 21st century CE as a <see cref="long"/>.
        /// </summary>
        internal static readonly long CenturyUnixTime = 978307200L;

        /// <summary>
        /// The birth date of Alan Turing as a <see cref="DateTime"/>.
        /// </summary>
        internal static readonly DateTime TuringDateTime = new DateTime(1954, 6, 23);

        /// <summary>
        /// The birth date of Alan Turing as a <see cref="long"/>.
        /// </summary>
        internal static readonly long TuringUnixTime = -489974400;

    }
}
