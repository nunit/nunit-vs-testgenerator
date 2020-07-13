// ***********************************************************************
// Copyright (c) 2015-2020 Terje Sandstrom
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

namespace TestGeneration.Extensions.NUnit
{
    /// <summary>
    /// The provider for the NUnit 2 unit test framework.
    /// </summary>
    [Export(typeof(IFrameworkProvider))]
    public class NUnit2FrameworkProvider : FrameworkProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnit2FrameworkProvider"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="configurationSettings">The configuration settings object to be used to determine how the test method is generated.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        /// <param name="directory">The directory object to use for directory operations.</param>
        [ImportingConstructor]
        public NUnit2FrameworkProvider(IServiceProvider serviceProvider, IConfigurationSettings configurationSettings, INaming naming, IDirectory directory)
            : base(new NUnit2SolutionManager(serviceProvider, naming, directory), new NUnitUnitTestProjectManager(serviceProvider, naming), new NUnitUnitTestClassManager(configurationSettings, naming))
        {
        }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        public override string Name => "NUnit2";

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        public override string AssemblyName => "nunit.framework";
    }
}