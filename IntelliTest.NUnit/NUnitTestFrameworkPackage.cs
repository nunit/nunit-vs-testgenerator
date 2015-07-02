// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NUnitTestFrameworkPackage.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   Package declaration for NUnit test framework.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Microsoft.Pex.Framework.Packages;

using Samples.Extensions.NUnit;

[assembly: PexPackageType(typeof(NUnitTestFrameworkPackage))]

namespace Samples.Extensions.NUnit
{
    using Microsoft.ExtendedReflection.ComponentModel;
    using Microsoft.Pex.Engine.ComponentModel;
    using Microsoft.Pex.Engine.TestFrameworks;

    using Microsoft.Pex.Framework.Packages;

    /// <summary>
    /// Sample extensions package for NUnit.
    /// </summary>
    public class NunitTestFrameworkPackageAttribute : PexPackageAttributeBase
    {
        protected override void Initialize(IEngine engine)
        {
            base.Initialize(engine);

            var testFrameworkService = engine.GetService<IPexTestFrameworkManager>();
            var host = testFrameworkService as IPexComponent;

            testFrameworkService.AddTestFramework(new NUnitTestFramework(host));
        }

        public override string Name => "NUnitTestFrameworkPackage";
    }

    [NunitTestFrameworkPackage]
    static class NUnitTestFrameworkPackage
    {
    }
}
