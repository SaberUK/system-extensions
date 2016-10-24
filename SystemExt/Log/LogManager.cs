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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SystemExt.Log
{

    /// <summary>
    /// Manager for logging systems.
    /// </summary>
    public sealed class LogManager
    {

        /// <summary>
        /// The loggers which have been registered with the manager.
        /// </summary>
        private readonly List<LoggerInfo> Loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogManager"/> class.
        /// </summary>
        public LogManager()
        {
            this.Loggers = new List<LoggerInfo>();
        }

        /// <summary>
        /// Add a logger to the manager.
        /// </summary>
        /// <param name="components">
        /// The components this logger will receive.
        /// </param>
        /// <param name="level">
        /// The level of messages this logger will receive.
        /// </param>
        /// <param name="logger">
        /// The logger to add.
        /// </param>
        public void AddLogger(string components, LogLevel level, ILogger logger)
        {
            this.RemoveLogger(logger);
            this.Loggers.Add(new LoggerInfo(components, level, logger));
        }

        /// <summary>
        /// Removes a logger from the manager.
        /// </summary>
        /// <param name="logger">
        /// The logger to remove.
        /// </param>
        public void RemoveLogger(ILogger logger)
        {
            foreach (var loggerInfo in this.Loggers.ToList().Where(loggerInfo => loggerInfo.Logger == logger))
            {
                this.Loggers.Remove(loggerInfo);
            }
        }

        /// <summary>
        /// Writes a message to the loggers.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The component which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        public void Write(LogLevel level, string component, string message, params object[] args)
        {
            // Format the message here to prevent duplication in the loggers.
            var formattedMessage = string.Format(message, args);

            // Iterate over the loggers and write to the ones which can log at this level.
            foreach (var loggerInfo in this.Loggers.Where(loggerInfo => loggerInfo.CanLog(level, component)))
            {
                loggerInfo.Logger.Write(component, formattedMessage);
            }
        }

        /// <summary>
        /// Writes a message to the loggers.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The object which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        public void Write(LogLevel level, object component, string message, params object[] args)
        {
            this.Write(level, component.GetType().Name, message, args);
        }

        /// <summary>
        /// Write a message to the loggers if DEBUG was defined at compile time.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The component which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        [Conditional("DEBUG")]
        public void WriteDebug(LogLevel level, string component, string message, params object[] args)
        {
            this.Write(level, component, message, args);
        }

        /// <summary>
        /// Write a message to the loggers if DEBUG was defined at compile time.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The object which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        [Conditional("DEBUG")]
        public void WriteDebug(LogLevel level, object component, string message, params object[] args)
        {
            this.Write(level, component.GetType().Name, message, args);
        }

        /// <summary>
        /// Write a message to the loggers if RELEASE was defined at compile time.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The component which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        [Conditional("RELEASE")]
        public void WriteRelease(LogLevel level, string component, string message, params object[] args)
        {
            this.Write(level, component, message, args);
        }

        /// <summary>
        /// Write a message to the loggers if RELEASE was defined at compile time.
        /// </summary>
        /// <param name="level">
        /// The level at which this message is to be logged.
        /// </param>
        /// <param name="component">
        /// The object which is logging this message.
        /// </param>
        /// <param name="message">
        /// A message to write to the loggers.
        /// </param>
        /// <param name="args">
        /// A variable number of arguments to format <paramref name="message"/> with.
        /// </param>
        [Conditional("RELEASE")]
        public void WriteRelease(LogLevel level, object component, string message, params object[] args)
        {
            this.Write(level, component.GetType().Name, message, args);
        }
    }
}
