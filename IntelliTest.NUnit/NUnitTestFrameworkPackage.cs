// ***********************************************************************
// Copyright (c) 2015 Terje Sandstrom
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
using Microsoft.Pex.Framework.Packages;


using TestGeneration.Extensions.IntelliTest.NUnit;


[assembly: PexPackageType(typeof(NUnitTestFrameworkPackage))]

namespace TestGeneration.Extensions.IntelliTest.NUnit
{
    using Microsoft.ExtendedReflection.ComponentModel;
    using Microsoft.Pex.Engine.ComponentModel;
    using Microsoft.Pex.Engine.TestFrameworks;

    using Microsoft.Pex.Framework.Packages;

   


    public class NunitTestFrameworkPackageAttribute : PexPackageAttributeBase
    {
        protected override void Initialize(IEngine engine)
        {
            base.Initialize(engine);

            var testFrameworkService = engine.GetService<IPexTestFrameworkManager>();
            var host = testFrameworkService as IPexComponent;

            testFrameworkService.AddTestFramework(new NUnit3TestFramework(host));
            testFrameworkService.AddTestFramework(new NUnit2TestFramework(host));
        }
    

        public override string Name => nameof(NUnitTestFrameworkPackage);
    }

    [NunitTestFrameworkPackage]
    static class NUnitTestFrameworkPackage
    {
    }
}
