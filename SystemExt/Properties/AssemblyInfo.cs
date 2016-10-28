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

using System.Reflection;
using System.Runtime.CompilerServices;

// Application
[assembly: AssemblyDescription("Implements useful extensions for the .NET Base Class Library.")]
[assembly: AssemblyProduct("System Extensions")]
[assembly: AssemblyTitle("System Extensions")]
[assembly: AssemblyVersion("0.4.2.*")]

// Author
[assembly: AssemblyCompany("Peter \"SaberUK\" Powell")]
[assembly: AssemblyCopyright("Copyright (C) 2014-2016 Peter \"SaberUK\" Powell")]
[assembly: AssemblyTrademark("Copyright (C) 2014-2016 Peter \"SaberUK\" Powell")]

// Unit Testing
[assembly: InternalsVisibleTo("SystemExt.Tests")]

#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
    [assembly: AssemblyConfiguration("Release")]
#else
    [assembly: AssemblyConfiguration("Unknown")]
#endif
