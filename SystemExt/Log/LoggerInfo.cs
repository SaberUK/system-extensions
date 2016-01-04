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

namespace SystemExt.Log
{

    /// <summary>
    /// Encapsulates information about a logger.
    /// </summary>
    internal sealed class LoggerInfo
    {

        /// <summary>
        /// The token list which decides what <see cref="Logger"/> should log.
        /// </summary>
        internal readonly TokenList Components;

        /// <summary>
        /// The level at which <see cref="Logger"/> should log.
        /// </summary>
        internal readonly LogLevel Level;

        /// <summary>
        /// The logger to write messages to.
        /// </summary>
        internal readonly ILogger Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerInfo"/> class
        /// </summary>
        /// <param name="components">
        /// A component filter for what <paramref name="logger"/> should log.
        /// </param>
        /// <param name="level">
        /// The level at which <paramref name="logger"/> should log.
        /// </param>
        /// <param name="logger">
        /// The logger to write messages to.
        /// </param>
        public LoggerInfo(string components, LogLevel level, ILogger logger)
        {
            this.Components = new TokenList(components.Split(' '));
            this.Level = level;
            this.Logger = logger;
        }

        /// <summary>
        /// Determines whether <see cref="Logger"/> can receive a log message.
        /// </summary>
        /// <param name="level">
        /// The level to test for loggability.
        /// </param>
        /// <param name="component">
        /// The component to test for loggability.
        /// </param>
        /// <returns>
        /// True if we can log this component at this level. Otherwise, false.
        /// </returns>
        public bool CanLog(LogLevel level, string component)
        {
            return this.Level >= level && this.Components.HasToken(component);
        }
    }
}
