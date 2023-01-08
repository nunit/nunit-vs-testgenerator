// ***********************************************************************
// Copyright (c) 2015-2018 Terje Sandstrom
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
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TestPlatform.TestGeneration;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Logging;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;
using VSLangProj80;

namespace TestGeneration.Extensions.NUnit
{
    /// <summary>
    /// A solution manager for NUnit unit tests.
    /// </summary>
    public class NUnit2SolutionManager : SolutionManagerBase
    {
        private const string NUnitVersion = "2.6.4";
        private const string NunitAdapterVersion = "2.2.0";
        private const string TestSDKVersion = "17.4.1";

        /// <summary>
        /// Initializes a new instance of the <see cref="NUnit2SolutionManager"/> class.
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
                throw new ArgumentNullException(nameof(unitTestProject));
            }

            TraceLogger.LogInfo("NUnitSolutionManager.OnUnitTestProjectCreated: Adding reference to NUnit 2 assemblies through nuget.");

            base.OnUnitTestProjectCreated(unitTestProject, sourceMethod);
            EnsureNuGetReference(unitTestProject, "NUnit", NUnitVersion);
            EnsureNuGetReference(unitTestProject, "NUnitTestAdapter", NunitAdapterVersion);
            EnsureNuGetReference(unitTestProject, "Microsoft.NET.Test.Sdk", TestSDKVersion);

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
