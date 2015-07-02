//---------------------------------------------------------------------
// <copyright file="NUnitUnitTestClassManager.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// <summary>The NUnitUnitTestClassManager type.</summary>
//---------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestPlatform.TestGeneration.Extensions.NUnit
{
    using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
    using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

    /// <summary>
    /// A unit test class for NUnit unit tests.
    /// </summary>
    public class NUnitUnitTestClassManager : UnitTestClassManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitUnitTestClassManager"/> class.
        /// </summary>
        /// <param name="configurationSettings">The configuration settings object to be used to determine how the test method is generated.</param>
        /// <param name="naming">The object to be used to give names to test projects.</param>
        public NUnitUnitTestClassManager(IConfigurationSettings configurationSettings, INaming naming)
            : base(configurationSettings, naming)
        {
        }

        /// <summary>
        /// The attribute name for marking a class as a test class.
        /// </summary>
        public override string TestClassAttribute
        {
            get { return "TestFixture"; }
        }

        /// <summary>
        /// The attribute name for marking a method as a test.
        /// </summary>
        public override string TestMethodAttribute
        {
            get { return "Test"; }
        }

        /// <summary>
        /// The code to force a test failure.
        /// </summary>
        public override string AssertionFailure
        {
            get { return "Assert.Fail()"; }
        }
    }
}
