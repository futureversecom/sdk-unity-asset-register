// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using AssetRegister.Runtime.RequestBuilder;
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
#if USING_UNITASK
		UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default);
#else
		IEnumerator Execute(IClient client, string authToken = null, Action<IResponse> callback = null);
#endif
	}

	public interface ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData> : IBuilder
		where TModel : class, IModel 
		where TInput : class, IInput
		where TBuilder : ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData>
		where TParentBuilder : IMainBuilder<TParentBuilder, TData>
		where TData : IData
	{
		TParentBuilder Done();
		TBuilder WithInput(TInput arguments);
		TBuilder WithField<TField>(Expression<Func<TModel, TField>> fieldExpression);

		IUnionBuilder<UnionBuilder<TBuilder, TField, TModel, TInput, TBuilder, TParentBuilder, TData>, TBuilder, TField,
			TModel, TInput, TBuilder, TParentBuilder, TData> WithUnionField<TField>(
			Expression<Func<TModel, TField>> fieldExpression) where TField : IUnion;
	}

	public interface IQuerySubBuilder<TModel, TInput, TParentBuilder, TData>
		: ISubBuilder<TModel, TInput, IQuerySubBuilder<TModel, TInput, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TInput : class, IInput 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IQueryData { }

	public interface IMutationSubBuilder<TModel, TInput, TParentBuilder, TData>
		: ISubBuilder<TModel, TInput, IMutationSubBuilder<TModel, TInput, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TInput : class, IInput 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IMutationData
	{
		IMutationSubBuilder<TModel, TInput, TParentBuilder, TData> WithFunctionName(string functionName);
	}

	public interface IMainBuilder<out TBuilder, in TData> : IBuilder 
		where TBuilder : IMainBuilder<TBuilder, TData> 
		where TData : IData
	{
		TBuilder RegisterData(TData data);
	}

	public interface IQueryBuilder : IMainBuilder<IQueryBuilder, IQueryData>
	{
		IQuerySubBuilder<TModel, TInput, IQueryBuilder, IQueryData> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : class, IModel 
			where TInput : class, IInput;
	}

	public interface IMutationBuilder : IMainBuilder<IMutationBuilder, IMutationData>
	{
		IMutationSubBuilder<TModel, TInput, IMutationBuilder, IMutationData> Add<TModel, TInput>(IMutation<TModel, TInput> mutation)
			where TModel : class, IModel 
			where TInput : class, IInput;
	}

	public interface IUnionBuilder<out TUnionBuilder, out TSubBuilder, in TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>
		where TUnionBuilder : IUnionBuilder<TUnionBuilder, TSubBuilder, TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>
		where TSubBuilder : ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData>
		where TUnion : IUnion
		where TModel : class, IModel
		where TInput : class, IInput
		where TBuilder : ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData>
		where TParentBuilder : IMainBuilder<TParentBuilder, TData>
		where TData : IData
	{
		TSubBuilder Done();

		TUnionBuilder As<TField, TUnionType>(Expression<Func<TUnionType, TField>> fieldExpression)
			where TUnionType : TUnion;
	}
}