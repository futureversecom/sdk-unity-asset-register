// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
#if USING_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else
using System.Collections;
#endif

namespace AssetRegister.Runtime.Interfaces
{
	public interface IBuilder
	{
		IRequest Build();
		UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default);
	}

	public interface ISubBuilder<out TBuilder> : IBuilder where TBuilder : IBuilder
	{
		TBuilder Done();
	}

	public interface IQueryBuilder : IBuilder
	{
		IQuerySubBuilder<IQueryBuilder, TSchema> Add<TSchema, TInput>(IQuery<TSchema, TInput> query)
			where TSchema : class, IModel where TInput : class, IInput;
	}

	public interface IQuerySubBuilder<out TBuilder, TSchema> : ISubBuilder<TBuilder>
		where TBuilder : IBuilder where TSchema : class, ISchema
	{
		IQuerySubBuilder<TBuilder, TSchema> WithField<TField>(Expression<Func<TSchema, TField>> fieldSelector);
		//IQuerySubBuilder<TBuilder, TModel> WithFragment(Fragment<TModel> fragment);
		IUnionSubBuilder<IQuerySubBuilder<TBuilder, TSchema>, TField> WithUnion<TField>(
			Expression<Func<TSchema, TField>> fieldSelector) where TField : class, IUnion;
		IInterfaceSubBuilder<IQuerySubBuilder<TBuilder, TSchema>, TField> WithInterface<TField>(
			Expression<Func<TSchema, TField>> fieldExpression) where TField : IInterface;
	}

	public interface IUnionSubBuilder<out TBuilder, in TUnion> : ISubBuilder<TBuilder> where TBuilder : IBuilder where TUnion : class, IUnion
	{
		public IQuerySubBuilder<IUnionSubBuilder<TBuilder, TUnion>, TUnionType> On<TUnionType>()
			where TUnionType : class, TUnion;
	}
	
	public interface IInterfaceSubBuilder<out TBuilder, TInterface> : ISubBuilder<TBuilder> where TBuilder : IBuilder where TInterface : IInterface
	{
		public IQuerySubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType> As<TInterfaceType>()
			where TInterfaceType : class, IInterface;
		IInterfaceSubBuilder<TBuilder, TInterface> WithField<TField>(Expression<Func<TInterface, TField>> fieldExpression);
	}
}