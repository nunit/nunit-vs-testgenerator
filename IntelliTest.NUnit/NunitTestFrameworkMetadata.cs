// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NunitTestFrameworkMetadata.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   Metadata for the NUnit framework.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Samples.Extensions.NUnit
{
    using Microsoft.ExtendedReflection.Metadata.Names;
    using Microsoft.ExtendedReflection.Utilities.Safe.Diagnostics;

    /// <summary>
    /// The metadata.
    /// </summary>
    internal static class NUnitTestFrameworkMetadata
    {
        internal static readonly ShortAssemblyName AssemblyName = ShortAssemblyName.FromName("nunit.framework");

        internal static readonly string RootNamespace = "NUnit.Framework";

        public static TypeName AttributeName(string name)
        {
            return TypeDefinitionName.FromName(
                AssemblyName,
                -1,
                false,
                RootNamespace,
                name + "Attribute").SelfInstantiation;
        }

        private static TypeName TypeName(string name)
        {
            SafeDebug.AssumeNotNullOrEmpty(name, "name");
            return TypeDefinitionName.FromName(
                AssemblyName,
                -1,
                false,
                RootNamespace,
                name).SelfInstantiation;
        }

        public static readonly TypeName AssertType = NUnitTestFrameworkMetadata.TypeName("Assert");
        public static readonly TypeDefinitionName AssertTypeDefinition = AssertType.Definition;
        public static readonly TypeName CollectionAssertType = NUnitTestFrameworkMetadata.TypeName("CollectionAssert");
        public static readonly TypeDefinitionName CollectionAssertTypeDefinition = CollectionAssertType.Definition;
    }
}