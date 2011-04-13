// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;

// The following are untriaged violations, do not add to this list unless you hit a bug in Code Analysis. Any explicitly 
// suppressed violations should either be applied against the member or type itself, or if raised against a namespace, 
// resource or assembly, placed in SuppressMessages.cs.

// Code Analysis Bug: ValidateArgumentsOfPublicMethods should not fire on protected members
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ComposablePartExportProvider.#GetExportsCore(System.ComponentModel.Composition.Primitives.ImportDefinition,System.ComponentModel.Composition.Hosting.AtomicComposition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.AggregateExportProvider.#GetExportsCore(System.ComponentModel.Composition.Primitives.ImportDefinition,System.ComponentModel.Composition.Hosting.AtomicComposition)")]

// Code Analysis Bug: ValidateArgumentsOfPublicMethods should not fire on usage of Requires.NotNull
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#AddExportedValue`1(System.ComponentModel.Composition.Hosting.CompositionBatch,System.String,!!0)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#AddPart(System.ComponentModel.Composition.Hosting.CompositionBatch,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#ComposeExportedValue`1(System.ComponentModel.Composition.Hosting.CompositionContainer,System.String,!!0)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#ComposeExportedValue`1(System.ComponentModel.Composition.Hosting.CompositionContainer,!!0)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#ComposeParts(System.ComponentModel.Composition.Hosting.CompositionContainer,System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.AttributedModelServices.#SatisfyImportsOnce(System.ComponentModel.Composition.ICompositionService,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.CatalogExportProvider+CatalogChangeProxy.#GetExports(System.ComponentModel.Composition.Primitives.ImportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.CatalogExportProvider.#SourceProvider")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Primitives.ComposablePartCatalog.#GetExports(System.ComponentModel.Composition.Primitives.ImportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Primitives.ComposablePartException.#GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ComposablePartExportProvider.#Compose(System.ComponentModel.Composition.Hosting.CompositionBatch)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.CompositionContainer.#ReleaseExports(System.Collections.Generic.IEnumerable`1<System.ComponentModel.Composition.Primitives.Export>)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.CompositionContainer.#ReleaseExports`2(System.Collections.Generic.IEnumerable`1<System.Lazy`2<!!0,!!1>>)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.CompositionContainer.#ReleaseExports`1(System.Collections.Generic.IEnumerable`1<System.Lazy`1<!!0>>)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.CompositionError.#.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.CompositionError.#GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.CompositionException.#GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition.#IsConstraintSatisfiedBy(System.ComponentModel.Composition.Primitives.ExportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ExportProvider.#GetExports(System.ComponentModel.Composition.Primitives.ImportDefinition,System.ComponentModel.Composition.Hosting.AtomicComposition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ExportProvider.#OnExportsChanged(System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ExportProvider.#OnExportsChanging(System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.ImportEngine.#.ctor(System.ComponentModel.Composition.Hosting.ExportProvider,System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo.#.ctor(System.Reflection.MemberInfo)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#GetExportingMember(System.ComponentModel.Composition.Primitives.ExportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#GetImportingMember(System.ComponentModel.Composition.Primitives.ImportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#GetImportingParameter(System.ComponentModel.Composition.Primitives.ImportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#GetPartType(System.ComponentModel.Composition.Primitives.ComposablePartDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#IsDisposalRequired(System.ComponentModel.Composition.Primitives.ComposablePartDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.#IsImportingParameter(System.ComponentModel.Composition.Primitives.ImportDefinition)")]
[module: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "System.ComponentModel.Composition.Hosting.TypeCatalog.#GetExports(System.ComponentModel.Composition.Primitives.ImportDefinition)")]


[module: SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "System.ComponentModel.Composition.ChangeRejectedException")]
[module: SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Scope = "type", Target = "System.ComponentModel.Composition.CompositionException")]
[module: SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "System.ComponentModel.Composition.CompositionException")]
[module: SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope = "member", Target = "System.ComponentModel.Composition.CompositionException+CompositionExceptionData.#System.Runtime.Serialization.ISafeSerializationData.CompleteDeserialization(System.Object)")]
[module: SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "System.ComponentModel.Composition.MetadataViewGenerator.#GenerateInterfaceViewProxyType(System.Type)")]



