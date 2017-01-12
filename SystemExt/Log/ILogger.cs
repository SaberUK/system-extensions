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
    /// Defines methods to be implemented by loggers.
    /// </summary>
    public interface ILogger
    {

        /// <summary>
        /// Write a message to this logger.
        /// </summary>
        /// <param name="component">
        /// The component which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        void Write(string component, string message);

    }
}
