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

using SystemExt.Terminal;

namespace SystemExt.Demo
{

    /// <summary>
    /// Main class.
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        private static int Main(string[] args)
        {
            return new ApplicationChooser()
                .AddEntryPoint(Log.EntryPoint, "Run the SystemExt.Log demo")
                .AddEntryPoint(Network.EntryPoint, "Run the SystemExt.Network demo")
                .Run(args);
        }
    }
}
