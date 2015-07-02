//---------------------------------------------------------------------
// <copyright file="NUnitUnitTestProjectManager.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// <summary>The NUnitUnitTestProjectManager type.</summary>
//---------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestPlatform.TestGeneration.Extensions.NUnit
{
    using System;
    using EnvDTE;
    using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

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
