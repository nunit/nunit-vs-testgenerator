// Copyright (c) Terje Sandstrom and Contributors. MIT License - see LICENSE.txt

using System;
using EnvDTE;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

namespace TestGeneration.Extensions.NUnit
{
    /// <summary>
    /// A unit test project for NUnit unit tests.
    /// </summary>
    public class NUnitUnitTestProjectManager : UnitTestProjectManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitUnitTestProjectManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        public NUnitUnitTestProjectManager(IServiceProvider serviceProvider, INaming naming)
            : base(serviceProvider, naming)
        {
        }

        /// <summary>
        /// Returns the full namespace that contains the test framework code elements for a given source project.
        /// </summary>
        /// <param name="sourceProject">The source project.</param>
        /// <returns>The full namespace that contains the test framework code elements.</returns>
        public override string FrameworkNamespace(Project sourceProject)
        {
            return "NUnit.Framework";
        }
    }
}
