﻿/**
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

using System.Reflection;

// Application
[assembly: AssemblyDescription("Unit Tests for System Extensions.")]
[assembly: AssemblyProduct("System Extensions")]
[assembly: AssemblyTitle("System Extensions Unit Tests")]

// Author
[assembly: AssemblyCompany("Peter \"SaberUK\" Powell")]
[assembly: AssemblyCopyright("Copyright (C) 2014-2017 Peter \"SaberUK\" Powell")]

#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
    [assembly: AssemblyConfiguration("Release")]
#else
    [assembly: AssemblyConfiguration("Unknown")]
#endif
