/**
 * System Extensions
 *
 *   Copyright (C) 2014-2015 Peter "SaberUK" Powell <petpow@saberuk.com>
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

namespace SystemExt.Extensions
{

    /// <summary>
    /// Extension methods for the <see cref="IList{T}"/> interface.
    /// </summary>
    public static class System_Collections_Generic_IList_T
    {

        /// <summary>
        /// Adds the elements of the specified enumerable to the end of the <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the element in the collection.
        /// </typeparam>
        /// <param name="self">
        /// The <see cref="IList{T}"/> to add the elements in <paramref name="enumerable"/> tp
        /// </param>
        /// <param name="enumerable">
        /// An enumerable list of <typeparamref name="T"/> to add to <paramref name="self"/>.
        /// </param>
        public static void AddRange<T>(this IList<T> self, IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
                self.Add(item);
        }

        /// <summary>
        /// Returns the element at a specified index in a sequence or the given value if the
        /// index is out of range.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the elements in <paramref name="self"/>.
        /// </typeparam>
        /// <param name="self">
        /// An <see cref="IList{T}"/> to return an element from.
        /// </param>
        /// <param name="index">
        /// The zero-based index of the element to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The value to return if the index is out of range.
        /// </param>
        /// <returns>
        /// The element requested from <paramref name="self"/> or <paramref name="defaultValue"/>
        /// if it does not exist.
        /// </returns>
        public static T ElementAtOrValue<T>(this IList<T> self, int index, T defaultValue)
        {
            return index >= 0 && index < self.Count ? self[index] : defaultValue;
        }
    }
}
