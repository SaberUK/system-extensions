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

namespace SystemExt.Log
{

    /// <summary>
    /// An enumeration of levels at which a message can be logged.
    /// </summary>
    public enum LogLevel
    {

        /// <summary>
        /// Log nothing.
        /// </summary>
        None,

        /// <summary>
        /// Log only critical messages.
        /// </summary>
        Critical,

        /// <summary>
        /// Log error messages and above.
        /// </summary>
        Error,

        /// <summary>
        /// Log warning messages and above.
        /// </summary>
        Warning,

        /// <summary>
        /// Log information messages and above.
        /// </summary>
        Information,

        /// <summary>
        /// Log all messages.
        /// </summary>
        Verbose,
    }
}
