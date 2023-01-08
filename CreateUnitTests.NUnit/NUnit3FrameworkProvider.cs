// Copyright (c) Terje Sandstrom and Contributors. MIT License - see LICENSE.txt

using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

namespace TestGeneration.Extensions.NUnit
{
    /// <summary>
    /// The provider for the NUnit 3 unit test framework.
    /// </summary>
    [Export(typeof(IFrameworkProvider))]
    public class NUnit3FrameworkProvider : FrameworkProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnit3FrameworkProvider"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="configurationSettings">The configuration settings object to be used to determine how the test method is generated.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        /// <param name="directory">The directory object to use for directory operations.</param>
        [ImportingConstructor]
        public NUnit3FrameworkProvider(IServiceProvider serviceProvider, IConfigurationSettings configurationSettings, INaming naming, IDirectory directory)
            : base(new NUnit3SolutionManager(serviceProvider, naming, directory), new NUnitUnitTestProjectManager(serviceProvider, naming), new NUnitUnitTestClassManager(configurationSettings, naming))
        {
        }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        public override string Name => "NUnit3";

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        public override string AssemblyName => "nunit.framework";
    }
}
