// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegisterV2.Runtime.Schemas;

namespace Plugins.AssetRegisterV2.Runtime.Builder
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
		IQuerySubBuilder<IQueryBuilder, TModel> Add<TModel>();
	}

	public interface IQuerySubBuilder<out TBuilder, TModel> : ISubBuilder<TBuilder> where TBuilder : IBuilder
	{
		IQuerySubBuilder<TBuilder, TModel> WithField<TField>(Expression<Func<TModel, TField>> fieldSelector);
		IQuerySubBuilder<TBuilder, TModel> WithFragment(Fragment<TModel> fragment);
		IUnionSubBuilder<IQuerySubBuilder<TBuilder, TModel>, TField> WithUnion<TField>(
			Expression<Func<TModel, TField>> fieldSelector) where TField : IUnion;
		IInterfaceSubBuilder<IQuerySubBuilder<TBuilder, TModel>, TField> WithInterface<TField>(
			Expression<Func<TModel, TField>> fieldExpression) where TField : IInterface;
	}

	public interface IUnionSubBuilder<out TBuilder, in TUnion> : ISubBuilder<TBuilder> where TBuilder : IBuilder where TUnion : IUnion
	{
		public IQuerySubBuilder<IUnionSubBuilder<TBuilder, TUnion>, TUnionType> As<TUnionType>()
			where TUnionType : TUnion;
	}
	
	public interface IInterfaceSubBuilder<out TBuilder, TInterface> : ISubBuilder<TBuilder> where TBuilder : IBuilder where TInterface : IInterface
	{
		public IQuerySubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType> As<TInterfaceType>()
			where TInterfaceType : IInterface;
		IInterfaceSubBuilder<TBuilder, TInterface> WithField<TField>(Expression<Func<TInterface, TField>> fieldExpression);
	}

	public interface IQueryAssembler
	{
		void RegisterToken(IToken token);
		void RegisterParameter(IParameter parameter);
	}
}