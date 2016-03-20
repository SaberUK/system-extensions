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

namespace SystemExt
{

    /// <summary>
    /// Represents the result of a function which may not exist.
    /// </summary>
    /// <typeparam name="T">
    /// The type which this <see cref="Option{T}"/> wraps.
    /// </typeparam>
    public sealed class Option<T> : IDisposable
    {

        /// <summary>
        /// Whether this <see cref="Option{T}"/> has a value.
        /// </summary>
        public readonly bool HasValue;

        /// <summary>
        /// The instance returned when no value is available.
        /// </summary>
        public static readonly Option<T> None = new Option<T>(); 

        /// <summary>
        /// The value this <see cref="Option{T}"/> is wrapping.
        /// </summary>
        private readonly T Value;

        /// <summary>
        /// Constructor used by the <see cref="Option{T}.None"/> instance.
        /// </summary>
        private Option()
        {
            this.HasValue = false;
            this.Value = default(T);
        }

        /// <summary>
        /// Constructor used when there is a value.
        /// </summary>
        /// <param name="value">
        /// The value this <see cref="Option{T}"/> is wrapping.
        /// </param>
        private Option(T value)
        {
            this.HasValue = true;
            this.Value = value;
        }
        
        /// <summary>
        /// Implicitly conversion from <typeparamref name="T"/> to <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The value the <see cref="Option{T}"/> should wrap.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="Option{T}"/> class.
        /// </returns>
        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }

        /// <summary>
        /// If this <see cref="Option{T}"/> is not <see cref="None"/> then call the function and return the result.
        /// Otherwise, return <see cref="Option{T2}.None"/>.
        /// </summary>
        /// <typeparam name="T2">
        /// The return type of <paramref name="func"/>.
        /// </typeparam>
        /// <param name="func">
        /// The function to call if this <see cref="Option{T}"/> is not null.
        /// </param>
        /// <returns>
        /// Either an <see cref="Option{T2}"/> which either wraps the result of the function call, or <see cref="Option{T2}.None"/>.
        /// </returns>
        public Option<T2> AndThen<T2>(Func<T, T2> func)
        {
            return this.HasValue ? Option<T2>.FromThrowingFunc(() => func(this.Value)) : Option<T2>.None;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // If we have no value then we have nothing to do here.
            if (!this.HasValue)
                return;

            // If we are not an IDisposable the we can't dispose resources.
            var disposable = this.Value as IDisposable;
            if (disposable == null)
                return;

            disposable.Dispose();
        }

        /// <summary>
        /// Wraps a value in an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The value the <see cref="Option{T}"/> should wrap.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="Option{T}"/> class.
        /// </returns>
        public static Option<T> Create(T value)
        {
            return new Option<T>(value);
        }

        /// <summary>
        /// Converts a <see cref="Nullable{T2}"/> value to an <see cref="Option{T2}"/>.
        /// </summary>
        /// <typeparam name="T2">
        /// The type which is being wrapped by the <see cref="Nullable{T2}"/>.
        /// </typeparam>
        /// <param name="value">
        /// A nullable value to convert to an <see cref="Option{T2}"/>.
        /// </param>
        /// <returns>
        /// A new option which wraps the value in <paramref name="value"/>.
        /// </returns>
        public static Option<T2> FromNullable<T2>(T2? value)
            where T2 : struct
        {
            return value.HasValue ? new Option<T2>(value.Value) : Option<T2>.None;
        }

        /// <summary>
        /// Execute a function which might throw and wraps the result in an Option{T2}.
        /// </summary>
        /// <param name="func">
        /// The function to wrap the result of.
        /// </param>
        /// <returns>
        /// The result of executing <paramref name="func"/> or <see cref="None"/> if it throws an exception.
        /// </returns>
        public static Option<T2> FromThrowingFunc<T2>(Func<T2> func)
        {
            try
            {
                var result = func();
                return new Option<T2>(result);
            }
            catch (Exception)
            {
                return Option<T2>.None;
            }
        }

        /// <summary>
        /// Retrieves the wrapped value if it exists and throws an <see cref="InvalidOperationException"/> if it does not.
        /// </summary>
        /// <returns>
        /// The value which is wrapped by this <see cref="Option{T}"/>.
        /// </returns>
        public T Get()
        {
            if (!this.HasValue)
                Environment.FailFast(string.Format("Get was called on an empty SystemEx.Option<{0}>!", typeof(T)));

            return this.Value;
        }

        /// <summary>
        /// Retrieves the wrapped value if it exists or the specified value if it does not.
        /// </summary>
        /// <param name="value">
        /// The value to return if there is no wrapped value.
        /// </param>
        /// <returns>
        /// The value which is wrapped by this <see cref="Option{T}"/> or <paramref name="value"/> if it does not exist.
        /// </returns>
        public T GetOr(T value)
        {
            return this.HasValue ? this.Value : value;
        }

        /// <summary>
        /// Retrieves the wrapped value if it exists or the return value of the specified function if it does not.
        /// </summary>
        /// <param name="func">
        /// The function to return the result of if there is no wrapped value.
        /// </param>
        /// <returns>
        /// The value which is wrapped by this <see cref="Option{T}"/> or the result of <paramref name="func"/> if it does not exist.
        /// </returns>
        public T GetOr(Func<T> func)
        {
            return this.HasValue ? this.Value : func();
        }

        /// <summary>
        /// Attempts to retrieve the wrapped value and return a value which represents whether it was retrieved.
        /// </summary>
        /// <param name="value">
        /// A reference to the variable to store the value of the wrapped value in.
        /// </param>
        /// <returns>
        /// True if this <see cref="Option{T}"/> has a value. Otherwise, false.
        /// </returns>
        public bool TryGet(out T value)
        {
            value = this.Value;
            return this.HasValue;
        }
    }
}
