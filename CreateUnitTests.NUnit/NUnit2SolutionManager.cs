//---------------------------------------------------------------------
// <copyright file="NUnitSolutionManager.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// <summary>The NUnitSolutionManager type.</summary>
//---------------------------------------------------------------------

using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TestPlatform.TestGeneration;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Logging;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;
using VSLangProj;
using VSLangProj80;

namespace TestGeneration.Extensions.NUnit
{
    /// <summary>
    /// A solution manager for NUnit unit tests.
    /// </summary>
    public class NUnit2SolutionManager : SolutionManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitSolutionManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        /// <param name="directory">The directory object to use for directory operations.</param>
        public NUnit2SolutionManager(IServiceProvider serviceProvider, INaming naming, IDirectory directory)
            : base(serviceProvider, naming, directory)
        {
        }

        /// <summary>
        /// Performs any preparatory tasks that have to be done after a new unit test project has been created.
        /// </summary>
        /// <param name="unitTestProject">The <see cref="Project"/> of the unit test project that has just been created.</param>
        /// <param name="sourceMethod">The <see cref="CodeFunction2"/> of the source method that is to be unit tested.</param>
        protected override void OnUnitTestProjectCreated(Project unitTestProject, CodeFunction2 sourceMethod)
        {
            if (unitTestProject == null)
            {
                throw new ArgumentNullException("unitTestProject");
            }

            TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Adding reference to NUnit 2 assemblies through nuget.");

            base.OnUnitTestProjectCreated(unitTestProject, sourceMethod);
            this.EnsureNuGetReference(unitTestProject, "NUnit", "2.6.4");

            var vsp = unitTestProject.Object as VSProject2;
            if (vsp != null)
            {
                var reference = vsp.References.Find(GlobalConstants.MSTestAssemblyName);
                if (reference != null)
                {
                    TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Removing reference to {0}", reference.Name);
                    reference.Remove();
                }
            }
        }
    }


    public class NUnitSolutionManager : SolutionManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitSolutionManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        /// <param name="directory">The directory object to use for directory operations.</param>
        public NUnitSolutionManager(IServiceProvider serviceProvider, INaming naming, IDirectory directory)
            : base(serviceProvider, naming, directory)
        {
        }

        /// <summary>
        /// Performs any preparatory tasks that have to be done after a new unit test project has been created.
        /// </summary>
        /// <param name="unitTestProject">The <see cref="Project"/> of the unit test project that has just been created.</param>
        /// <param name="sourceMethod">The <see cref="CodeFunction2"/> of the source method that is to be unit tested.</param>
        protected override void OnUnitTestProjectCreated(Project unitTestProject, CodeFunction2 sourceMethod)
        {
            if (unitTestProject == null)
            {
                throw new ArgumentNullException("unitTestProject");
            }

            TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Adding reference to NUnit assemblies through nuget.");

            base.OnUnitTestProjectCreated(unitTestProject, sourceMethod);
            this.EnsureNuGetReference(unitTestProject, "NUnit", "3.0.0-beta-2");

            var vsp = unitTestProject.Object as VSProject2;
            if (vsp != null)
            {
                var reference = vsp.References.Find(GlobalConstants.MSTestAssemblyName);
                if (reference != null)
                {
                    TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Removing reference to {0}", reference.Name);
                    reference.Remove();
                }
            }
        }
    }
}
