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

namespace SystemExt.Extensions
{

    /// <summary>
    /// Extension methods for the <see cref="Stream"/> class.
    /// </summary>
    public static class System_IO_Stream
    {

        /// <summary>
        /// Determines whether the end of the stream has been reached.
        /// </summary>
        /// <param name="self">
        /// An instance of the <see cref="Stream"/> class.
        /// </param>
        /// <returns>
        /// Tf the end of the stream has been reached then true; otherwise, false.
        /// </returns>
        public static bool HasReachedEnd(this Stream self)
        {
            return self.Position == self.Length;
        }
    }
}
