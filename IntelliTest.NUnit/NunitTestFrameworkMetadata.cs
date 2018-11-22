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
namespace TestGeneration.Extensions.IntelliTest.NUnit
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

        public static readonly TypeName AssertType = TypeName("Assert");
        public static readonly TypeDefinitionName AssertTypeDefinition = AssertType.Definition;
        public static readonly TypeName CollectionAssertType = TypeName("CollectionAssert");
        public static readonly TypeDefinitionName CollectionAssertTypeDefinition = CollectionAssertType.Definition;
    }
}