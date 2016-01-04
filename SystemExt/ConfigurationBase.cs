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

using System.IO;
using System.Xml.Serialization;

namespace SystemExt
{

    /// <summary>
    /// Base class for a class containing configuration properties.
    /// </summary>
    public abstract class ConfigurationBase<T>
        where T : ConfigurationBase<T>, new()
    {

        /// <summary>
        /// Load the configuration from disk.
        /// </summary>
        /// <param name="path">
        /// The location to load from.
        /// </param>
        /// <param name="missingOkay">
        /// Whether a new configuration should be created if the file doesn't exist.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="T"/> class.
        /// </returns>
        public static T Load(string path, bool missingOkay = true)
        {
            // If the file doesn't exist then we don't need to load it.
            if (!File.Exists(path) && missingOkay)
                return new T();

            // Create a serializer instance and load the file.
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Write the configuration to disk.
        /// </summary>
        /// <param name="path">
        /// The location to save to.
        /// </param>
        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
