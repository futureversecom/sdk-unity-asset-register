// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.RequestBuilder
{
	public class UnionBuilder<TSubBuilder, TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>
		: IUnionBuilder<UnionBuilder<TSubBuilder, TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>, TSubBuilder,
			TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>
		where TSubBuilder : ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData>
		where TUnion : IUnion
		where TModel : class, IModel
		where TInput : class, IInput
		where TBuilder : ISubBuilder<TModel, TInput, TBuilder, TParentBuilder, TData>
		where TParentBuilder : IMainBuilder<TParentBuilder, TData>
		where TData : IData
	{
		private readonly TSubBuilder _subBuilder;
		
		public UnionBuilder(TSubBuilder subBuilder)
		{
			_subBuilder = subBuilder;
		}
		
		public TSubBuilder Done()
		{
			return _subBuilder;
		}

		public UnionBuilder<TSubBuilder, TUnion, TModel, TInput, TBuilder, TParentBuilder, TData>
			As<TField, TUnionType>(Expression<Func<TUnionType, TField>> fieldExpression) where TUnionType : TUnion
		{
			return this;
		}
	}
}