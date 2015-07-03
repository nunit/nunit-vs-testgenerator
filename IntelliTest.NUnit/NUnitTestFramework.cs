// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NUnitTestFramework.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   NUnit test framework
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Samples.Extensions.NUnit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Microsoft.ExtendedReflection.Asserts;
    using Microsoft.ExtendedReflection.Collections;
    using Microsoft.ExtendedReflection.Metadata;
    using Microsoft.ExtendedReflection.Metadata.Names;
    using Microsoft.ExtendedReflection.Monitoring;
    using Microsoft.ExtendedReflection.Utilities;
    using Microsoft.ExtendedReflection.Utilities.Safe;
    using Microsoft.ExtendedReflection.Utilities.Safe.Diagnostics;
    using Microsoft.Pex.Engine.ComponentModel;
    using Microsoft.Pex.Engine.TestFrameworks;

    /// <summary>
    /// NUnit test framework
    /// </summary>
    [Serializable]
    sealed class NUnitTestFramework : AttributeBasedTestFrameworkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitTestFramework"/> class.
        /// </summary>
        /// <param name="host">
        /// </param>
        public NUnitTestFramework(IPexComponent host)
            : base(host)
        { }

        /// <summary>
        /// identify of the test framework
        /// </summary>
        /// <value></value>
        public override string Name => "NUnit";

        /// <summary>
        /// Gets the assembly name of the framework main's assembly. This name is used
        /// to automatically discover test frameworks, based the assembly references
        /// </summary>
        /// <value></value>
        public override ShortAssemblyName AssemblyName => NUnitTestFrameworkMetadata.AssemblyName;

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <value>The root namespace.</value>
        public override string RootNamespace
        {
            get { return NUnitTestFrameworkMetadata.RootNamespace; }
        }

        /// <summary>
        /// The test framework references.
        /// </summary>
        public override ICountable<ShortReferenceAssemblyName> References
        {
            get
            {
                return Indexable.One(new ShortReferenceAssemblyName(ShortAssemblyName.FromName("Nunit"), "2.6.4", AssemblyReferenceType.NugetReference));
            }
        }

        /// <summary>
        /// The _directory.
        /// </summary>
        private string _directory = null;

        /// <summary>
        /// Hint on the location of the test framework assembly
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool TryGetDirectory(out string directory)
        {
            if (this._directory == null)
            {
                DirectoryInfo programFiles = new DirectoryInfo(Environment.ExpandEnvironmentVariables("%ProgramFiles%"));
                DirectoryInfo[] info = programFiles.GetDirectories("NUnit-Net-*", SearchOption.TopDirectoryOnly);
                if (info == null || info.Length == 0)
                    this._directory = string.Empty;
                else
                    this._directory = Path.Combine(info[0].FullName, "bin");
            }

            directory = this._directory;
            return !SafeString.IsNullOrEmpty(directory);
        }

        /// <summary>
        /// Gets a value indicating whether
        /// partial test classes
        /// </summary>
        /// <value></value>
        public override bool SupportsPartialClasses => true;

        /// <summary>
        /// The supports project bitness.
        /// </summary>
        /// <param name="bitness">
        /// The bitness.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool SupportsProjectBitness(Bitness bitness)
        {
            SafeDebug.Assume(bitness != Bitness.Unsupported, "bitness != Bitness.Unsupported");
            return true;
        }

        /// <summary>
        /// The _expected exception attribute.
        /// </summary>
        [NonSerialized]
        TypeName _expectedExceptionAttribute;

        /// <summary>
        /// Gets the ExpectedException attribute.
        /// </summary>
        /// <value>The expected exception attribute.</value>
        public override TypeName ExpectedExceptionAttribute => this._expectedExceptionAttribute ??
                                                               (this._expectedExceptionAttribute = NUnitTestFrameworkMetadata.AttributeName("ExpectedException"));

        /// <summary>
        /// Tries the read expected exception.
        /// </summary>
        /// <param name="target">
        /// The method.
        /// </param>
        /// <param name="exceptionType">
        /// Type of the exception.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool TryReadExpectedException(
            ICustomAttributeProviderEx target, 
            out TypeEx exceptionType)
        {
            object attribute = AttributeHelper.GetAttribute(target, this.ExpectedExceptionAttribute);
            if (attribute != null)
            {
                Type attributeType = attribute.GetType();

                // read exception type using reflection.
                var field = attributeType.GetField("expectedException", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    Type t = field.GetValue(attribute) as Type;
                    bool isClass;
                    if (t != null && ReflectionHelper.TryGetIsClass(t, out isClass) && isClass
                        && !ReflectionHelper.ContainsGenericParameters(t))
                    {
                        exceptionType = MetadataFromReflection.GetType(t);
                        return true;
                    }
                }
            }

            exceptionType = null;
            return false;
        }

        /// <summary>
        /// Tries to get the assembly set up tear down attribute.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="setUp">
        /// The set up.
        /// </param>
        /// <param name="tearDown">
        /// The tear down.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool TryGetAssemblySetupTeardownMethods(
            AssemblyEx assembly, 
            out Method setUp, 
            out Method tearDown)
        {
            setUp = null;
            tearDown = null;
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether[fixture set up tear down are instance methods.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [fixture set up tear down instance]; otherwise, <c>false</c>.
        /// </value>
        public override bool FixtureSetupTeardownInstance
        {
            get { return true; }
        }

        /// <summary>
        /// The _fixture attribute.
        /// </summary>
        [NonSerialized]
        TypeName _fixtureAttribute;

        /// <summary>
        /// Gets the name of the fixture attribute
        /// </summary>
        /// <value>The fixture attribute.</value>
        public override TypeName FixtureAttribute => this._fixtureAttribute ??
                                                     (this._fixtureAttribute = NUnitTestFrameworkMetadata.AttributeName("TestFixture"));

        /// <summary>
        /// The _fixture set up attribute.
        /// </summary>
        [NonSerialized]
        TypeName _fixtureSetUpAttribute;

        /// <summary>
        /// Gets the name of the fixture setup attribute
        /// </summary>
        /// <value>The fixture set up attribute.</value>
        public override TypeName FixtureSetupAttribute => this._fixtureSetUpAttribute ??
                                                          (this._fixtureSetUpAttribute = NUnitTestFrameworkMetadata.AttributeName("TestFixtureSetUp"));

        /// <summary>
        /// The _fixture tear down attribute.
        /// </summary>
        [NonSerialized]
        TypeName _fixtureTearDownAttribute;

        /// <summary>
        /// Gets the name of the fixture teardown attribute
        /// </summary>
        /// <value>The fixture tear down attribute.</value>
        public override TypeName FixtureTeardownAttribute => this._fixtureTearDownAttribute ??
                                                             (this._fixtureTearDownAttribute = NUnitTestFrameworkMetadata.AttributeName("TestFixtureTearDown"));

        /// <summary>
        /// The _set up attribute.
        /// </summary>
        [NonSerialized]
        TypeName _setUpAttribute;

        /// <summary>
        /// Gets the name of the test setup attribute.
        /// </summary>
        /// <value>The set up attribute.</value>
        public override TypeName SetupAttribute => this._setUpAttribute ??
                                                   (this._setUpAttribute = NUnitTestFrameworkMetadata.AttributeName("SetUp"));

        /// <summary>
        /// The _test attribute.
        /// </summary>
        [NonSerialized]
        TypeName _testAttribute;

        /// <summary>
        /// Gets the name of the test attribute.
        /// </summary>
        /// <value>The set up attribute.</value>
        public override TypeName TestAttribute => this._testAttribute ?? (this._testAttribute = NUnitTestFrameworkMetadata.AttributeName("Test"));

        /// <summary>
        /// The _tear down attribute.
        /// </summary>
        [NonSerialized]
        TypeName _tearDownAttribute;

        /// <summary>
        /// Gets the name of the test teardown attribute.
        /// </summary>
        /// <value>The tear down attribute.</value>
        public override TypeName TeardownAttribute => this._tearDownAttribute ??
                                                      (this._tearDownAttribute = NUnitTestFrameworkMetadata.AttributeName("TearDown"));

        /// <summary>
        /// The _ignore attribute.
        /// </summary>
        [NonSerialized]
        TypeName _ignoreAttribute;

        /// <summary>
        /// Gets the ignore attribute.
        /// </summary>
        /// <value>The ignore attribute.</value>
        public override TypeName IgnoreAttribute => this._ignoreAttribute ??
                                                    (this._ignoreAttribute = NUnitTestFrameworkMetadata.AttributeName("Ignore"));

        /// <summary>
        /// Whether the ignore attribute constructor takes a message as its first argument.
        /// </summary>
        /// <value></value>
        protected override bool HasIgnoreAttributeMessage => true;

        /// <summary>
        /// Gets the ignore message property.
        /// </summary>
        /// <value>The ignore message property.</value>
        protected override string IgnoreMessageProperty => "Reason";

        /// <summary>
        /// Gets the expected exception property name.
        /// </summary>
        /// <value>The expected exception property.</value>
        protected override string ExpectedExceptionProperty => "ExceptionType";

        /// <summary>
        /// Gets a list of attribute that should be duplicated from the
        /// pex test to the parameterized test
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        protected override IEnumerable<TypeName> GetSatelliteAttributeTypes()
        {
            return Indexable.Array<TypeName>(
                this.CategoryAttribute, 
                NUnitTestFrameworkMetadata.AttributeName("Description"), 
                NUnitTestFrameworkMetadata.AttributeName("Explicit"), 
                NUnitTestFrameworkMetadata.AttributeName("Platform"), 
                NUnitTestFrameworkMetadata.AttributeName("Property")
                );
        }

        /// <summary>
        /// The _category attribute.
        /// </summary>
        [NonSerialized]
        TypeName _categoryAttribute;

        /// <summary>
        /// Gets the category attribute.
        /// </summary>
        private TypeName CategoryAttribute => this._categoryAttribute ??
                                              (this._categoryAttribute = NUnitTestFrameworkMetadata.AttributeName("Category"));

        /// <summary>
        /// Tries the get categories.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="names">
        /// The names.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool TryGetCategories(
            ICustomAttributeProviderEx element, 
            out IEnumerable<string> names)
        {
            SafeDebug.AssumeNotNull(element, "element");

            // TODO
            names = null;
            return false;
        }

        /// <summary>
        /// The _assertion exception type.
        /// </summary>
        [NonSerialized]
        TypeName _assertionExceptionType;

        /// <summary>
        /// Gets the type of the assertion exception.
        /// </summary>
        /// <value>The type of the assertion exception.</value>
        public override TypeName AssertionExceptionType
        {
            get
            {
                System.Diagnostics.Debugger.Launch();
                if (this._assertionExceptionType == null)
                    this._assertionExceptionType = TypeDefinitionName.FromName(
                        NUnitTestFrameworkMetadata.AssemblyName, 
                        -1, 
                        false, 
                        NUnitTestFrameworkMetadata.RootNamespace, 
                        "AssertionException").SelfInstantiation;
                return this._assertionExceptionType;
            }
        }

        /// <summary>
        /// Gets a value indicating whether supports static test methods.
        /// </summary>
        public override bool SupportsStaticTestMethods => false;

        /// <summary>
        /// Gets the assert method filters.
        /// </summary>
        public override IIndexable<IAssertMethodFilter> AssertMethodFilters => Indexable.One<IAssertMethodFilter>(NUnitAssertMethodFilter.Instance);
    }
}