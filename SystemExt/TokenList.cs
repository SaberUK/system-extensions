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

namespace SystemExt
{

    /// <summary>
    /// Implements a token list.
    /// </summary>
    public sealed class TokenList
    {

        /// <summary>
        /// Whether this list includes all tokens by default.
        /// </summary>
        private bool IsPermissive;

        /// <summary>
        /// The tokens to exclude if <see cref="IsPermissive"/> is true, or include if
        /// <see cref="IsPermissive"/> is false.
        /// </summary>
        private readonly List<string> Tokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenList"/> class with the specified token list.
        /// </summary>
        /// <param name="tokens">
        /// A list of tokens.
        /// </param>
        public TokenList(IEnumerable<string> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException("tokens");

            this.Tokens = new List<string>();
            foreach (var token in tokens.Where(token => !string.IsNullOrWhiteSpace(token)))
            {
                // Tokens that begin with a hyphen should be denied.
                if (token[0] == '-')
                    this.RemoveToken(token.Substring(1));
                else
                    this.AddToken(token);
            }
        }

        /// <summary>
        /// Add the specified token to the list.
        /// </summary>
        /// <param name="token">
        /// The token to add.
        /// </param>
        public void AddToken(string token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            // Check whether this token is a wildcard entry.
            if (token == "*")
            {
                this.IsPermissive = true;
                this.Tokens.Clear();
                return;
            }

            // If we are in permissive mode then remove the token from the
            // token list. Otherwise, add it to the token list.
            if (this.IsPermissive)
                this.Tokens.Remove(token);
            else
                this.Tokens.Add(token);
        }

        /// <summary>
        /// Determines whether a token exists in the token list.
        /// </summary>
        /// <param name="token">
        /// The token to search for.
        /// </param>
        /// <returns>
        /// True if the token exists in the list; otherwise, false.
        /// </returns>
        public bool HasToken(string token)
        {
            // If we are in permissive mode and the token is in the list
            // then we don't have it.
            if (this.IsPermissive && this.Tokens.Contains(token))
                return false;

            // If we are not in permissive mode and the token is not in
            // the list then we don't have it.
            if (!this.IsPermissive && !this.Tokens.Contains(token))
                return false;

            // We have the token!
            return true;
        }

        /// <summary>
        /// Remove the specified token from the list.
        /// </summary>
        /// <param name="token">
        /// The token to remove.
        /// </param>
        public void RemoveToken(string token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            // Check whether this token is a wildcard entry.
            if (token == "*")
            {
                this.IsPermissive = false;
                this.Tokens.Clear();
                return;
            }

            // If we are in permissive mode then add the token to the
            // token list. Otherwise, remove it from the token list.
            if (this.IsPermissive)
                this.Tokens.Add(token);
            else
                this.Tokens.Remove(token);
        }
    }
}
