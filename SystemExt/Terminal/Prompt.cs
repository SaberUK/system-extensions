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

using System;
using System.Globalization;

namespace SystemExt.Terminal
{

    /// <summary>
    /// Encapsulates methods for prompting for input on the command line.
    /// </summary>
    public static class Prompt
    {

        /// <summary>
        /// Prompts the user to enter a boolean answer to a question.
        /// </summary>
        /// <param name="question">
        /// The question to ask the user.
        /// </param>
        /// <param name="defaultValue">
        /// The answer to return if the user just presses enter.
        /// </param>
        /// <returns>
        /// The response given by the user converted to a boolean.
        /// </returns>
        public static bool Boolean(string question, bool defaultValue = false)
        {
            while (true)
            {
                var input = Prompt.String(question, defaultValue ? "yes" : "no");
                switch (input)
                {
                    case "1":
                    case "on":
                    case "yes":
                    case "y":
                        return true;

                    case "0":
                    case "no":
                    case "n":
                    case "off":
                        return false;

                    default:
                        Console.Error.WriteLine("You must enter a boolean value (i.e. yes or no).");
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user to enter a float answer to a question.
        /// </summary>
        /// <param name="question">
        /// The question to ask the user.
        /// </param>
        /// <param name="defaultValue">
        /// The answer to return if the user just presses enter.
        /// </param>
        /// <param name="min">
        /// The minimum possible value.
        /// </param>
        /// <param name="max">
        /// The maximum possible value.
        /// </param>
        /// <returns>
        /// The response given by the user converted to a boolean.
        /// </returns>
        public static double Float(string question, double defaultValue = 0, double min = long.MinValue, double max = long.MaxValue)
        {
            while (true)
            {
                var input = Prompt.String(question, defaultValue.ToString(CultureInfo.InvariantCulture));

                double result;
                if (!double.TryParse(input, out result) || result < min || result > max)
                {
                    Console.Error.WriteLine("You must enter a valid decimal number between {0} and {1}.", min, max);
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Prompts the user to enter an integer answer to a question.
        /// </summary>
        /// <param name="question">
        /// The question to ask the user.
        /// </param>
        /// <param name="defaultValue">
        /// The answer to return if the user just presses enter.
        /// </param>
        /// <param name="min">
        /// The minimum possible value.
        /// </param>
        /// <param name="max">
        /// The maximum possible value.
        /// </param>
        /// <returns>
        /// The response given by the user converted to a boolean.
        /// </returns>
        public static long Integer(string question, long defaultValue = 0, long min = long.MinValue, long max = long.MaxValue)
        {
            while (true)
            {
                var input = Prompt.String(question, defaultValue.ToString(CultureInfo.InvariantCulture));

                long result;
                if (!long.TryParse(input, out result) || result < min || result > max)
                {
                    Console.Error.WriteLine("You must enter a valid whole number between {0} and {1}.", min, max);
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Prompts the user to enter a textual answer to a question.
        /// </summary>
        /// <param name="question">
        /// The question to ask the user.
        /// </param>
        /// <param name="defaultValue">
        /// The answer to return if the user just presses enter.
        /// </param>
        /// <returns>
        /// The response given by the user.
        /// </returns>
        public static string String(string question, string defaultValue = null)
        {
            Console.WriteLine("{0}?", question);
            Console.Write("[{0}] => ", defaultValue);

            var input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }
    }
}
