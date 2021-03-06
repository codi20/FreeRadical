﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.AttributedModel;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.UnitTesting;
using System.Linq;
using System.Reflection;
using System.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.ComponentModel.Composition.ReflectionModel;

namespace Tests.Integration
{
    [TestClass]
    public class ExportFactoryTests
    {
        public interface IId
        {
            int Id { get; }
        }

        public interface IIdTypeMetadata
        {
            string IdType { get; }
            string ExportTypeIdentity { get; }
        }


        [Export(typeof(IId))]
        [ExportMetadata("IdType", "PostiveIncrement")]
        public class UniqueExport : IId, IDisposable
        {
            private static int lastId = 0;

            public UniqueExport()
            {
                Id = lastId++;
            }

            public int Id { get; private set; }

            public void Dispose()
            {
                Id = -1;
            }
        }

        [Export]
        [CLSCompliant(false)]
        public class ExportFactoryImporter
        {
            [ImportingConstructor]
            public ExportFactoryImporter(
                ExportFactory<IId> idCreatorTCtor,
                ExportFactory<IId, IIdTypeMetadata> idCreatorTMCtor)
            {
                this._idCreatorTCtor = idCreatorTCtor;
                this._idCreatorTMCtor = idCreatorTMCtor;
            }

            private ExportFactory<IId> _idCreatorTCtor;
            private ExportFactory<IId, IIdTypeMetadata> _idCreatorTMCtor;

            [Import(typeof(IId))]
            public ExportFactory<IId> _idCreatorTField = null; // public so these can work on SL

            [Import]
            public ExportFactory<IId, IIdTypeMetadata> _idCreatorTMField = null; // public so these can work on SL

            [Import]
            public ExportFactory<IId> IdCreatorTProperty { get; set; }

            [Import(typeof(IId))]
            public ExportFactory<IId, IIdTypeMetadata> IdCreatorTMProperty { get; set; }

            [ImportMany]
            public ExportFactory<IId>[] IdCreatorsTProperty { get; set; }

            [ImportMany]
            public ExportFactory<IId, IIdTypeMetadata>[] IdCreatorsTMProperty { get; set; }

            public void AssertValid()
            {
                var ids = new int[] 
                {
                    VerifyExportFactory(this._idCreatorTCtor),
                    VerifyExportFactory(this._idCreatorTMCtor),
                    VerifyExportFactory(this._idCreatorTField),
                    VerifyExportFactory(this._idCreatorTMField),
                    VerifyExportFactory(this.IdCreatorTProperty),
                    VerifyExportFactory(this.IdCreatorTMProperty),
                    VerifyExportFactory(this.IdCreatorsTProperty[0]),
                    VerifyExportFactory(this.IdCreatorsTMProperty[0])
                };

                Assert.AreEqual(1, this.IdCreatorsTProperty.Length, "Should only be one ExportFactory");
                Assert.AreEqual(1, this.IdCreatorsTMProperty.Length, "Should only be one ExportFactory");

                CollectionAssert.AllItemsAreUnique(ids, "There should be no duplicate ids");
            }

            private int VerifyExportFactory(ExportFactory<IId> creator)
            {
                var val1 = creator.CreateExport();
                var val2 = creator.CreateExport();

                Assert.AreNotEqual(val1.Value, val2.Value, "Values should not be the same");
                Assert.AreNotEqual(val1.Value.Id, val2.Value.Id, "Value Ids should not be the same");

                Assert.IsTrue(val1.Value.Id >= 0, "Id should be positive");

                val1.Dispose();

                Assert.IsTrue(val1.Value.Id < 0, "Disposal of the value should set the id to negative");

                return creator.CreateExport().Value.Id;
            }

            private int VerifyExportFactory(ExportFactory<IId, IIdTypeMetadata> creator)
            {
                var val = VerifyExportFactory((ExportFactory<IId>)creator);

                Assert.AreEqual("PostiveIncrement", creator.Metadata.IdType, "IdType should be PositiveIncrement");
                Assert.AreEqual(AttributedModelServices.GetTypeIdentity(typeof(ComposablePartDefinition)), creator.Metadata.ExportTypeIdentity);

                return val;
            }
        }

        [TestMethod]
        public void ExportFactoryStandardImports_ShouldWorkProperly()
        {
            var container = CreateWithAttributedCatalog(typeof(UniqueExport), typeof(ExportFactoryImporter));
            var partCreatorImporter = container.GetExportedValue<ExportFactoryImporter>();

            partCreatorImporter.AssertValid();
        }

        [Export]
        public class Foo : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                this.IsDisposed = true;
            }
        }

        [Export]
        public class SimpleExportFactoryImporter
        {
            [Import]
            public ExportFactory<Foo> FooFactory { get; set; }
        }

        [TestMethod]
        public void ExportFactoryOfT_RecompositionSingle_ShouldBlockChanges()
        {
            var aggCat = new AggregateCatalog();
            var typeCat = new TypeCatalog(typeof(Foo));
            aggCat.Catalogs.Add(new TypeCatalog(typeof(SimpleExportFactoryImporter)));
            aggCat.Catalogs.Add(typeCat);

            var container = new CompositionContainer(aggCat);

            var fooFactory = container.GetExportedValue<SimpleExportFactoryImporter>();

            ExceptionAssert.Throws<ChangeRejectedException>(() =>
                aggCat.Catalogs.Remove(typeCat));

            ExceptionAssert.Throws<ChangeRejectedException>(() =>
                aggCat.Catalogs.Add(new TypeCatalog(typeof(Foo))));
        }

        [Export]
        public class ManyExportFactoryImporter
        {
            [ImportMany(AllowRecomposition = true)]
            public ExportFactory<Foo>[] FooFactories { get; set; }
        }

        [TestMethod]
        public void FactoryOfT_RecompositionImportMany_ShouldSucceed()
        {
            var aggCat = new AggregateCatalog();
            var typeCat = new TypeCatalog(typeof(Foo));
            aggCat.Catalogs.Add(new TypeCatalog(typeof(ManyExportFactoryImporter)));
            aggCat.Catalogs.Add(typeCat);

            var container = new CompositionContainer(aggCat);

            var fooFactories = container.GetExportedValue<ManyExportFactoryImporter>();

            Assert.AreEqual(1, fooFactories.FooFactories.Length);

            aggCat.Catalogs.Add(new TypeCatalog(typeof(Foo)));

            Assert.AreEqual(2, fooFactories.FooFactories.Length);
        }

        public class ExportFactoryExplicitCP
        {
            [Import(RequiredCreationPolicy = CreationPolicy.Any)]
            public ExportFactory<Foo> FooCreatorAny { get; set; }

            [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
            public ExportFactory<Foo> FooCreatorNonShared { get; set; }

            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public ExportFactory<Foo> FooCreatorShared { get; set; }

            [ImportMany(RequiredCreationPolicy = CreationPolicy.Any)]
            public ExportFactory<Foo>[] FooCreatorManyAny { get; set; }

            [ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
            public ExportFactory<Foo>[] FooCreatorManyNonShared { get; set; }

            [ImportMany(RequiredCreationPolicy = CreationPolicy.Shared)]
            public ExportFactory<Foo>[] FooCreatorManyShared { get; set; }
        }

        [TestMethod]
        public void ExportFactory_ExplicitCreationPolicy_CPShouldBeIgnored()
        {
            var container = CreateWithAttributedCatalog(typeof(Foo));

            var part = new ExportFactoryExplicitCP();

            container.SatisfyImportsOnce(part);

            // specifying the required creation policy explicit on the import 
            // of a ExportFactory will be ignored because the ExportFactory requires
            // the part it wraps to be either Any or NonShared to work properly.
            Assert.IsNotNull(part.FooCreatorAny);
            Assert.IsNotNull(part.FooCreatorNonShared);
            Assert.IsNotNull(part.FooCreatorShared);

            Assert.AreEqual(1, part.FooCreatorManyAny.Length);
            Assert.AreEqual(1, part.FooCreatorManyNonShared.Length);
            Assert.AreEqual(1, part.FooCreatorManyShared.Length);
        }

        public class ExportFactoryImportRequiredMetadata
        {
            [ImportMany]
            public ExportFactory<Foo>[] FooCreator { get; set; }

            [ImportMany]
            public ExportFactory<Foo, IIdTypeMetadata>[] FooCreatorWithMetadata { get; set; }
        }

        [TestMethod]
        public void ExportFactory_ImportRequiredMetadata_MissingMetadataShouldCauseImportToBeExcluded()
        {
            var container = CreateWithAttributedCatalog(typeof(Foo));

            var part = new ExportFactoryImportRequiredMetadata();

            container.SatisfyImportsOnce(part);

            Assert.AreEqual(1, part.FooCreator.Length, "Should contain the one Foo");
            Assert.AreEqual(0, part.FooCreatorWithMetadata.Length, "Should NOT contain Foo because it is missing the required Id metadata property");
        }

        [Export(typeof(Foo))]
        [PartCreationPolicy(CreationPolicy.Shared)]
        public class SharedFoo : Foo
        {
        }

        [TestMethod]
        public void ExportFactory_ImportShouldNotImportSharedPart()
        {
            var container = CreateWithAttributedCatalog(typeof(SharedFoo));

            var foo = container.GetExportedValue<Foo>();
            Assert.IsNotNull(foo, "Ensure that a Foo actually exists in the container");

            var part = new ExportFactoryImportRequiredMetadata();

            container.SatisfyImportsOnce(part);

            Assert.AreEqual(0, part.FooCreator.Length, "Should not contain the SharedFoo because the ExportFactory should only wrap Any/NonShared parts");
        }


        [TestMethod]
        public void ExportFactory_QueryContainerDirectly_ShouldWork()
        {
            var container = CreateWithAttributedCatalog(typeof(Foo));

            var importDef = ReflectionModelServicesEx.CreateImportDefinition(
                new LazyMemberInfo(MemberTypes.Field, () => new MemberInfo[] { typeof(ExportFactoryTests) }), // Give it a bogus member
                AttributedModelServices.GetContractName(typeof(Foo)),
                AttributedModelServices.GetTypeIdentity(typeof(Foo)),
                Enumerable.Empty<KeyValuePair<string, Type>>(),
                ImportCardinality.ZeroOrMore,
                true,
                CreationPolicy.Any,
                true, // isExportFactory
                null);

            var exports = container.GetExports(importDef);

            var partCreator = exports.Single();

            // Manually walk the steps of using a raw part creator which is modeled as a PartDefinition with
            // a single ExportDefinition.
            var partDef = (ComposablePartDefinition)partCreator.Value;
            var part = partDef.CreatePart();
            var foo = (Foo)part.GetExportedValue(partDef.ExportDefinitions.Single());

            Assert.IsNotNull(foo);

            var foo1 = (Foo)part.GetExportedValue(partDef.ExportDefinitions.Single());
            Assert.AreEqual(foo, foo1, "Retrieving the exported value from the same part should return the same value");

            // creating a new part should result in getting a new exported value
            var part2 = partDef.CreatePart();
            var foo2 = (Foo)part2.GetExportedValue(partDef.ExportDefinitions.Single());

            Assert.AreNotEqual(foo, foo2, "New part should equate to a new exported value");

            // Disposing of part should cause foo to be disposed
            ((IDisposable)part).Dispose();
            Assert.IsTrue(foo.IsDisposed);
        }

        [Export]
        public class PartImporter<PartType>
        {
            [Import]
            public ExportFactory<PartType> Creator { get; set; }
        }

        [Export]
        public class SimpleExport
        {
        }

        [TestMethod]
        public void ExportFactory_SimpleRejectionRecurrection_ShouldWork()
        {
            var importTypeCat = new TypeCatalog(typeof(PartImporter<SimpleExport>));
            var aggCatalog = new AggregateCatalog(importTypeCat);
            var container = new CompositionContainer(aggCatalog);
            var exports = container.GetExports<PartImporter<SimpleExport>>();
            Assert.AreEqual(0, exports.Count());

            aggCatalog.Catalogs.Add(new TypeCatalog(typeof(SimpleExport)));

            exports = container.GetExports<PartImporter<SimpleExport>>();
            Assert.AreEqual(1, exports.Count());
        }


        private static CompositionContainer CreateWithAttributedCatalog(params Type[] types)
        {
            var catalog = new TypeCatalog(types);
            return new CompositionContainer(catalog);
        }
    }
}
