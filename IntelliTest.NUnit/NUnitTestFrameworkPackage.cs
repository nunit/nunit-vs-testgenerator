// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NUnitTestFrameworkPackage.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   Package declaration for NUnit test framework.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
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

            testFrameworkService.AddTestFramework(new NUnitTestFramework(host));
            testFrameworkService.AddTestFramework(new NUnit2TestFramework(host));
        }
    

        public override string Name => nameof(NUnitTestFrameworkPackage);
    }

    [NunitTestFrameworkPackage]
    static class NUnitTestFrameworkPackage
    {
    }
}
