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

using SystemExt.Log;
using SystemExt.Terminal;

namespace SystemExt.Demo
{

    /// <summary>
    /// Demo for <see cref="SystemExt.Log"/>.
    /// </summary>
    public static class Log
    {

        /// <summary>
        /// Entry point for the <see cref="SystemExt.Log"/> demo.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        public static int EntryPoint(string[] args)
        {
            return new ApplicationChooser()
                .AddEntryPoint(LogComponents, "Iterate through various component filters and write a message")
                .AddEntryPoint(LogLevels, "Iterate through all log levels and write a message")
                .Run(args);
        }

        /// <summary>
        /// A demo which iterates over a series of tokens and logs them to a stream.
        /// </summary>
        /// <param name="arg">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int LogComponents(string[] arg)
        {
            // Initialize manager and STDOUT logger.
            var manager = new LogManager();
            var logger = new StreamLogger(Console.OpenStandardOutput());

            // Iterate over various log tokens.
            foreach (var logToken in new[] { "*", "INVALID DEMO2", "DEMO1 -DEMO2", "* -DEMO2 -* INVALID" })
            {
                manager.AddLogger(logToken, LogLevel.Verbose, logger);
                Console.WriteLine("Component filter set to {0}", logToken);
                manager.Write(LogLevel.Verbose, "DEMO1", "Logging with the DEMO1 component!");
                manager.Write(LogLevel.Verbose, "DEMO2", "Logging with the DEMO2 component!");
                manager.Write(LogLevel.Verbose, manager, "Logging with the LogManager component!");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            return 0;
        }

        /// <summary>
        /// A demo which iterates over log levels and writes messages at each.
        /// </summary>
        /// <param name="arg">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int LogLevels(string[] arg)
        {
            // Initialize manager and STDOUT logger.
            var manager = new LogManager();
            var logger = new StreamLogger(Console.OpenStandardOutput());

            // Iterate through the log levels.
            for (var level = LogLevel.None; level <= LogLevel.Verbose; level++)
            {
                // Change the log level.
                Console.WriteLine("Setting log level to {0:G}.", level);
                manager.AddLogger("DEMO", level, logger);

                // Write messages to the logger at every level.
                manager.Write(LogLevel.Verbose, "DEMO", "Verbose!");
                manager.Write(LogLevel.Information, "DEMO", "Information!");
                manager.Write(LogLevel.Warning, "DEMO", "Warning!");
                manager.Write(LogLevel.Error, "DEMO", "Error!");
                manager.Write(LogLevel.Critical, "DEMO", "Critical!");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            return 0;
        }
    }
}
