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
			CancellationToken cancellationToken = default);
	}

	public interface ISubBuilder<out TBuilder> : IBuilder where TBuilder : IBuilder
	{
		TBuilder Done();
	}

	public interface IRequestBuilder<out TBuilder> : IBuilder where TBuilder : IRequestBuilder<TBuilder>
	{
		TBuilder SetHeader(string headerName, string value);
		TBuilder SetAuth(string authToken);
	}

	public interface IQueryBuilder : IRequestBuilder<IQueryBuilder>
	{
		IMemberSubBuilder<IQueryBuilder, TModel> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : IModel where TInput : class, IInput;
	}
	
	public interface IMutationBuilder : IRequestBuilder<IMutationBuilder>
	{
		IMemberSubBuilder<IMutationBuilder, TModel> Add<TModel, TInput>(IMutation<TModel, TInput> mutation)
			where TModel : IModel where TInput : class, IInput;
	}

	public interface IMemberSubBuilder<out TBuilder, TType> : ISubBuilder<TBuilder>
		where TBuilder : IBuilder
	{
		IMemberSubBuilder<TBuilder, TType> WithField<TField>(Expression<Func<TType, TField>> fieldSelector);
		IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithMethod<TField>(Expression<Func<TType, TField>> fieldSelector);
		IUnionSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithUnion<TField>(
			Expression<Func<TType, TField>> fieldSelector) where TField : class, IUnion;
		IInterfaceSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithInterface<TField>(
			Expression<Func<TType, TField>> fieldExpression) where TField : IInterface;
	}

	public interface IUnionSubBuilder<out TBuilder, in TUnion> : ISubBuilder<TBuilder>
		where TBuilder : IBuilder where TUnion : class, IUnion
	{
		public IMemberSubBuilder<IUnionSubBuilder<TBuilder, TUnion>, TUnionType> On<TUnionType>()
			where TUnionType : class, TUnion;
	}

	public interface IInterfaceSubBuilder<out TBuilder, TInterface> : IMemberSubBuilder<TBuilder, TInterface>
		where TBuilder : IBuilder where TInterface : IInterface
	{
		public IMemberSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType> On<TInterfaceType>()
			where TInterfaceType : IInterface;
	}
}