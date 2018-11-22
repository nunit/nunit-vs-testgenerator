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