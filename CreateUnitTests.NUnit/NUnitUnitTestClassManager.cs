// Copyright (c) Terje Sandstrom and Contributors. MIT License - see LICENSE.txt

using Microsoft.VisualStudio.TestPlatform.TestGeneration.Data;
using Microsoft.VisualStudio.TestPlatform.TestGeneration.Model;

namespace TestGeneration.Extensions.NUnit
{
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
        public override string TestClassAttribute => "TestFixture";

        /// <summary>
        /// The attribute name for marking a method as a test.
        /// </summary>
        public override string TestMethodAttribute => "Test";

        /// <summary>
        /// The code to force a test failure.
        /// </summary>
        public override string AssertionFailure => "Assert.Fail()";
    }
}
