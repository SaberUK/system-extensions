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
using System.Net;

using SystemExt.Network;
using SystemExt.Terminal;

namespace SystemExt.Demo
{

    /// <summary>
    /// Demo for <see cref="SystemExt.Terminal"/>.
    /// </summary>
    public static class Terminal
    {

        /// <summary>
        /// Entry point for the <see cref="SystemExt.Terminal"/> demo.
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
                .AddEntryPoint(PromptUsage, "Show the usage of the Prompt class")
                .Run(args);
        }

        /// <summary>
        /// A demo which shows how to use the Prompt class.
        /// </summary>
        /// <param name="arg">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int PromptUsage(string[] arg)
        {
            Console.WriteLine("Result: {0}", Prompt.Boolean("Boolean prompt"));
            Console.WriteLine("Result: {0}", Prompt.Float("Float prompt"));
            Console.WriteLine("Result: {0}", Prompt.Integer("Integer prompt"));
            Console.WriteLine("Result: {0}", Prompt.String("String prompt"));
            return 0;
        }
    }
}
