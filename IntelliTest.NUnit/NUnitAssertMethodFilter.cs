// Copyright (c) Terje Sandstrom and Contributors. MIT License - see LICENSE.txt

using Microsoft.ExtendedReflection.Asserts;
using Microsoft.ExtendedReflection.Metadata;
using Microsoft.ExtendedReflection.Utilities.Safe.Diagnostics;

namespace TestGeneration.Extensions.IntelliTest.NUnit
{
    /// <summary>
    /// The NUnit assert method filer.
    /// </summary>
    internal sealed class NUnitAssertMethodFilter : IAssertMethodFilter
    {
        private NUnitAssertMethodFilter() { }
        public static NUnitAssertMethodFilter Instance = new NUnitAssertMethodFilter();

        public bool IsAssertMethod(MethodDefinition method, out int usefulParameters)
        {
            SafeDebug.AssumeNotNull(method, "method");
            TypeDefinition type;
            if (method.TryGetDeclaringType(out type))
            {
                if (type.SerializableName == NUnitTestFrameworkMetadata.AssertTypeDefinition)
                {
                    switch (method.ShortName)
                    {
                        case "IsEmpty":
                        case "IsNotEmpty":
                        case "False":
                        case "IsFalse":
                        case "True":
                        case "IsTrue":
                        case "IsAssignableFrom":
                        case "IsNotAssignableFrom":
                        case "IsNull":
                        case "IsNotNull":
                        case "Null":
                        case "NotNull":
                        case "IsNotNullOrEmpty":
                        case "IsNullOrEmpty":
                        case "IsNan":
                        case "IsInstanceOf":    //not entirely correct
                        case "IsNotInstanceOf": //not entirely correct
                            usefulParameters = 1;
                            return true;
                        case "AreEqual":
                        case "AssertDoublesAreEqual":
                        case "AreNotEqual":
                        case "Contains":
                        case "AreSame":
                        case "AreNotSame":
                        case "Greater":
                        case "GreaterOrEqual":
                        case "Less":
                        case "LessOrEqual":
                            usefulParameters = 2;
                            return true;
                    }
                }
                else if (type.SerializableName == NUnitTestFrameworkMetadata.CollectionAssertTypeDefinition)
                {
                    switch (method.ShortName)
                    {
                        case "Equals":
                        case "ReferenceEquals":
                            usefulParameters = -1;
                            return false;
                        default:
                            usefulParameters = 0;
                            return true;
                    }
                }
            }
            usefulParameters = -1;
            return false;
        }
    }
}
