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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExt.Tests
{

    /// <summary>
    /// Implements tests for the <see cref="Option{T}"/> class.
    /// </summary>
    [TestClass]
    public sealed class SystemExt_Option_T_Test
    {

        /// <summary>
        /// Test the <see cref="Option{T}.Create"/> method.
        /// </summary>
        [TestMethod]
        public void Create()
        {
            const int value = 42;
            var result = Option<int>.Create(value);

            // Ensure that the option has a value.
            Assert.IsTrue(result.HasValue);

            // Ensure that we can retrieve the wrapped value.
            Assert.AreEqual(value, result.Get());
            Assert.AreEqual(value, result.GetOr(58));
            Assert.AreEqual(value, result.GetOr(() => 58));

            // Ensure that the and operation is called when we have a value.
            Assert.AreEqual(84, result.AndThen(number => number * 2).Get());
        }

        /// <summary>
        /// Test the <see cref="Option{T}.FromNullable{T2}"/> method.
        /// </summary>
        [TestMethod]
        public void FromNullable()
        {
            const int value = 42;

            // Ensure that a Nullable<T> with a value creates a valid Option<T>.
            int? hasValue = value;
            Assert.AreEqual(Option<int>.Create(value).Get(), Option<int>.FromNullable(hasValue).Get());

            // Ensure that a Nullable<T> with no value returns None.
            int? noValue = null;
            Assert.AreEqual(Option<int>.None, Option<int>.FromNullable(noValue));
        }

        /// <summary>
        /// Test the <see cref="Option{T}.FromThrowingFunc{T2}"/> method.
        /// </summary>
        [TestMethod]
        public void FromThrowingFunc()
        {
            const int value = 42;

            // Ensure that a normal return value creates a valid Option<T>.
            Assert.AreEqual(value, Option<int>.FromThrowingFunc(() => value).Get());

            // Ensure that an exception being thrown returns None.
            Assert.AreEqual(Option<int>.None, Option<int>.FromThrowingFunc<int>(() => { throw new Exception(); }));
        }

        /// <summary>
        /// Test the <see cref="Option{T}.None"/> value.
        /// </summary>
        [TestMethod]
        public void None()
        {
            var result = Option<int>.None;

            // Ensure that the option has no value.
            Assert.IsFalse(result.HasValue);

            // Ensure that an exception is thrown when trying to retrieve the value.
            try
            {
                result.Get();
                Assert.Fail("Calling Get() on Option{T}.None did not throw an InvalidOperationException!");
            }
            catch (InvalidOperationException) { }

            // Ensure that we get the default values.
            const int value = 42;
            Assert.AreEqual(value, result.GetOr(value));
            Assert.AreEqual(value, result.GetOr(() => value));

            // Ensure that the and operation is not called when we have no value.
            Assert.AreEqual(Option<int>.None, result.AndThen(number => number * 2));
        }
    }
}
