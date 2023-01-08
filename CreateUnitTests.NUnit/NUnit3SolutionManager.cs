// Copyright (c) Terje Sandstrom and Contributors. MIT License - see LICENSE.txt

using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TestPlatform.TestGeneration;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Logging;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;
using VSLangProj80;

namespace TestGeneration.Extensions.NUnit
{
    public class NUnit3SolutionManager : SolutionManagerBase
    {
        private const string NUnitVersion = "3.13.3";
        private const string NunitAdapterVersion = "4.3.1";
        private const string TestSDKVersion = "17.4.1";

        /// <summary>
        /// Initializes a new instance of the <see cref="NUnit3SolutionManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use to get the interfaces required.</param>
        /// <param name="naming">The naming object used to decide how projects, classes and methods are named and created.</param>
        /// <param name="directory">The directory object to use for directory operations.</param>
        public NUnit3SolutionManager(IServiceProvider serviceProvider, INaming naming, IDirectory directory)
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
                throw new ArgumentNullException(nameof(unitTestProject));
            }

            TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Adding reference to NUnit assemblies through nuget.");

            base.OnUnitTestProjectCreated(unitTestProject, sourceMethod);
            this.EnsureNuGetReference(unitTestProject, "NUnit", NUnitVersion);
            this.EnsureNuGetReference(unitTestProject, "NUnit3TestAdapter", NunitAdapterVersion);
            this.EnsureNuGetReference(unitTestProject, "Microsoft.NET.Test.Sdk", TestSDKVersion);

            var vsp = unitTestProject.Object as VSProject2;
            var reference = vsp?.References.Find(GlobalConstants.MSTestAssemblyName);
            if (reference != null)
            {
                TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Removing reference to {0}", reference.Name);
                reference.Remove();
            }
        }
    }
}
