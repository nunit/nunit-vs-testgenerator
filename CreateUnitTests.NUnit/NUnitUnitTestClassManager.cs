// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
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
