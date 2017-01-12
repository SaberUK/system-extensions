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
using System.Collections.Generic;
using System.Linq;

namespace SystemExt.Terminal
{

    /// <summary>
    /// Allows an application to have multiple entry points depending on user input.
    /// </summary>
    public sealed class ApplicationChooser
    {

        /// <summary>
        /// The index of the default entry.
        /// </summary>
        private int DefaultIndex;

        /// <summary>
        /// The entry points which have been registered.
        /// </summary>
        private readonly IList<KeyValuePair<Func<string[], int>, string>> EntryPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationChooser"/> class.
        /// </summary>
        public ApplicationChooser()
        {
            this.EntryPoints = new List<KeyValuePair<Func<string[], int>, string>>
            {
                new KeyValuePair<Func<string[], int>, string>(function => 1, "Exit without running an application")
            };
        }

        /// <summary>
        /// Register a potential entry point with the chooser.
        /// </summary>
        /// <param name="entryPoint">
        /// A potential entry point.
        /// </param>
        /// <param name="description">
        /// A description of this entry point.
        /// </param>
        /// <param name="makeDefault">
        /// Whether to make this entry point the default.
        /// </param>
        /// <returns>
        /// This instance of <see cref="ApplicationChooser"/>.
        /// </returns>
        public ApplicationChooser AddEntryPoint(Func<string[], int> entryPoint, string description, bool makeDefault = false)
        {
            if (entryPoint == null)
                throw new ArgumentNullException("entryPoint");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");

            this.EntryPoints.Add(new KeyValuePair<Func<string[], int>, string>(entryPoint, description));

            if (makeDefault)
                this.DefaultIndex = this.EntryPoints.Count - 1;

            return this;
        }

        /// <summary>
        /// Prompt the user for the entry point to run and then run it.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The code to terminate the application with on exit.
        /// </returns>
        public int Run(string[] args)
        {
            Console.WriteLine("Please choose the application you wish to run:");
            Console.WriteLine();

            // Display the possible entry points to the user.
            for (var i = 0; i < this.EntryPoints.Count; i++)
                Console.WriteLine("  [{0}] {1}.", i, this.EntryPoints.ElementAt(i).Value);

            while (true)
            {
                // Prompt the user to enter a value.
                Console.WriteLine();
                Console.Write("[{0}] => ", this.DefaultIndex);

                // Read the value from the user.
                var input = Console.ReadLine();

                // If the value is empty then use the default.
                var index = -1;
                if (string.IsNullOrWhiteSpace(input))
                    index = this.DefaultIndex;

                // If a value has been given ensure it is valid.
                if (index < 0 && (!int.TryParse(input, out index) || index < 0 || index >= this.EntryPoints.Count))
                {
                    Console.Error.WriteLine("You must enter a valid number between 0 and {0}.", this.EntryPoints.Count - 1);
                    continue;
                }

                // Call the entry point.
                return this.EntryPoints[index].Key(args);
            }
        }
    }
}
