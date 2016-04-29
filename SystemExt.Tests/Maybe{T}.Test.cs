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
    /// Implements tests for the <see cref="Maybe{T}"/> class.
    /// </summary>
    [TestClass]
    public sealed class SystemExt_Maybe_T_Test
    {

        /// <summary>
        /// A disposable class that we use for testing.
        /// </summary>
        public sealed class TestDisposable : IDisposable
        {

            /// <summary>
            /// Whether this class has been disposed.
            /// </summary>
            public bool Disposed;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this.Disposed = true;
            }
        }

        /// <summary>
        /// Test the <see cref="Maybe{T}.Create"/> method.
        /// </summary>
        [TestMethod]
        public void Create()
        {
            const int value = 42;
            var result = Maybe<int>.Create(value);

            // Ensure that the maybe has a value.
            Assert.IsTrue(result.HasValue);

            // Ensure that we can retrieve the wrapped value.
            Assert.AreEqual(value, result.Get());
            Assert.AreEqual(value, result.GetOr(58));
            Assert.AreEqual(value, result.GetOr(() => 58));

            var tryValue = int.MinValue;
            Assert.IsTrue(result.TryGet(out tryValue));
            Assert.AreEqual(value, tryValue);

            // Ensure that the and operation is called when we have a value.
            Assert.AreEqual(84, result.AndThen(number => number * 2).Get());
        }

        /// <summary>
        /// Test the <see cref="Maybe{T}.Dispose"/> method.
        /// </summary>
        [TestMethod]
        public void Dispose()
        {
            var maybe = Maybe<TestDisposable>.FromThrowingFunc(() => new TestDisposable());

            // Ensure that we can dispose the wrapped type correctly.
            maybe.Dispose();
            Assert.IsTrue(maybe.Get().Disposed);
        }

        /// <summary>
        /// Test the <see cref="Maybe{T}.FromNullable{T2}"/> method.
        /// </summary>
        [TestMethod]
        public void FromNullable()
        {
            const int value = 42;

            // Ensure that a Nullable<T> with a value creates a valid Maybe<T>.
            int? hasValue = value;
            Assert.AreEqual(Maybe<int>.Create(value).Get(), Maybe<int>.FromNullable(hasValue).Get());

            // Ensure that a Nullable<T> with no value returns None.
            int? noValue = null;
            Assert.AreEqual(Maybe<int>.None, Maybe<int>.FromNullable(noValue));
        }

        /// <summary>
        /// Test the <see cref="Maybe{T}.FromThrowingFunc{T2}"/> method.
        /// </summary>
        [TestMethod]
        public void FromThrowingFunc()
        {
            const int value = 42;

            // Ensure that a normal return value creates a valid Maybe<T>.
            Assert.AreEqual(value, Maybe<int>.FromThrowingFunc(() => value).Get());

            // Ensure that an exception being thrown returns None.
            Assert.AreEqual(Maybe<int>.None, Maybe<int>.FromThrowingFunc<int>(() => { throw new Exception(); }));
        }

        /// <summary>
        /// Test the <see cref="Maybe{T}.None"/> value.
        /// </summary>
        [TestMethod]
        public void None()
        {
            var result = Maybe<int>.None;

            // Ensure that the maybe has no value.
            Assert.IsFalse(result.HasValue);

            // Ensure that we get the default values.
            const int value = 42;
            Assert.AreEqual(value, result.GetOr(value));
            Assert.AreEqual(value, result.GetOr(() => value));

            var tryValue = int.MinValue;
            Assert.IsFalse(result.TryGet(out tryValue));
            Assert.AreEqual(default(int), tryValue);

            // Ensure that the and operation is not called when we have no value.
            Assert.AreEqual(Maybe<int>.None, result.AndThen(number => number * 2));
        }
    }
}
