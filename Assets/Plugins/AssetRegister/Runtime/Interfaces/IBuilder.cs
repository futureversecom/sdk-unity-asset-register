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
#if USING_UNITASK
		UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default);
#else
		IEnumerator Execute(IClient client, string authToken = null, Action<IResponse> callback = null);
#endif
	}

	public interface ISubBuilder<TModel, in TArgs, out TBuilder, out TParentBuilder, TData> : IBuilder
		where TModel : class, IModel 
		where TArgs : class, IArgs
		where TBuilder : ISubBuilder<TModel, TArgs, TBuilder, TParentBuilder, TData>
		where TParentBuilder : IMainBuilder<TParentBuilder, TData>
		where TData : IData
	{
		TParentBuilder Done();
		TBuilder WithArgs(TArgs arguments);
		TBuilder WithField<TField>(Expression<Func<TModel, TField>> fieldExpression);
	}

	public interface IQuerySubBuilder<TModel, in TArgs, out TParentBuilder, TData>
		: ISubBuilder<TModel, TArgs, IQuerySubBuilder<TModel, TArgs, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TArgs : class, IArgs 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IQueryData { }

	public interface IMutationSubBuilder<TModel, in TArgs, out TParentBuilder, TData>
		: ISubBuilder<TModel, TArgs, IMutationSubBuilder<TModel, TArgs, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TArgs : class, IArgs 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IMutationData
	{
		IMutationSubBuilder<TModel, TArgs, TParentBuilder, TData> WithFunctionName(string functionName);
	}

	public interface IMainBuilder<out TBuilder, in TData> : IBuilder 
		where TBuilder : IMainBuilder<TBuilder, TData> 
		where TData : IData
	{
		TBuilder RegisterData(TData data);
	}

	public interface IQueryBuilder : IMainBuilder<IQueryBuilder, IQueryData>
	{
		IQuerySubBuilder<TModel, TArgs, IQueryBuilder, IQueryData> Add<TModel, TArgs>(IQuery<TModel, TArgs> query)
			where TModel : class, IModel 
			where TArgs : class, IArgs;
	}

	public interface IMutationBuilder : IMainBuilder<IMutationBuilder, IMutationData>
	{
		IMutationSubBuilder<TModel, TArgs, IMutationBuilder, IMutationData> Add<TModel, TArgs>(IMutation<TModel, TArgs> mutation)
			where TModel : class, IModel 
			where TArgs : class, IArgs;
	}
}