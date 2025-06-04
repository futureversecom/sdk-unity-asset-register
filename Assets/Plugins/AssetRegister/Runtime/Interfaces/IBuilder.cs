// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public interface IBuilder
	{
		Request Build();
		UniTask<Result> Execute(IAssetRegisterClient client, string authToken, CancellationToken cancellationToken);
	}

	public interface ISubBuilder<TModel, in TArgs, out TBuilder, out TParentBuilder, TData> : IBuilder
		where TModel : class, IModel 
		where TArgs : class, IArguments
		where TBuilder : ISubBuilder<TModel, TArgs, TBuilder, TParentBuilder, TData>
		where TParentBuilder : IMainBuilder<TParentBuilder, TData>
		where TData : IQueryData
	{
		TParentBuilder Done();
		TBuilder WithArgs(TArgs arguments);
		TBuilder WithField<TField>(Expression<Func<TModel, TField>> fieldExpression);
	}

	public interface IQuerySubBuilder<TModel, in TArgs, out TParentBuilder, TData>
		: ISubBuilder<TModel, TArgs, IQuerySubBuilder<TModel, TArgs, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TArgs : class, IArguments 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IQueryData { }

	public interface IMutationSubBuilder<TModel, in TArgs, out TParentBuilder, TData>
		: ISubBuilder<TModel, TArgs, IMutationSubBuilder<TModel, TArgs, TParentBuilder, TData>, TParentBuilder, TData>
		where TModel : class, IModel 
		where TArgs : class, IArguments 
		where TParentBuilder : IMainBuilder<TParentBuilder, TData> 
		where TData : class, IMutationData
	{
		IMutationSubBuilder<TModel, TArgs, TParentBuilder, TData> WithFunctionName(string functionName);
	}

	public interface IMainBuilder<out TBuilder, in TData> : IBuilder 
		where TBuilder : IMainBuilder<TBuilder, TData> 
		where TData : IQueryData
	{
		TBuilder RegisterData(TData data);
	}

	public interface IQueryBuilder : IMainBuilder<IQueryBuilder, IQueryData>
	{
		IQuerySubBuilder<TModel, TArgs, IQueryBuilder, IQueryData> AddQuery<TModel, TArgs>(IQuery<TModel, TArgs> query)
			where TModel : class, IModel 
			where TArgs : class, IArguments;
	}

	public interface IMutationBuilder : IMainBuilder<IMutationBuilder, IMutationData>
	{
		IMutationSubBuilder<TModel, TArgs, IMutationBuilder, IMutationData> AddMutation<TModel, TArgs>(IMutation<TModel, TArgs> mutation)
			where TModel : class, IModel 
			where TArgs : class, IArguments;
	}
}