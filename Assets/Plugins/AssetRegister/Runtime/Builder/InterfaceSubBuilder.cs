// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	public class InterfaceSubBuilder<TBuilder, TInterface> : IInterfaceSubBuilder<TBuilder, TInterface>, IQueryAssembler, IToken 
		where TBuilder : IBuilder, IQueryAssembler 
		where TInterface : IInterface
	{
		private readonly TBuilder _parentBuilder;

		public InterfaceSubBuilder(TBuilder parentBuilder)
		{
			_parentBuilder = parentBuilder;
		}

		public TBuilder Done()
		{
			_parentBuilder.RegisterToken(this);
			return _parentBuilder;
		}

		public IRequest Build()
		{
			return Done().Build();
		}

		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await Done().Execute(client, authToken, cancellationToken);
		}

		public void RegisterToken(IToken token)
		{
			throw new NotImplementedException();
		}

		public void RegisterParameter(IParameter parameter)
		{
			_parentBuilder.RegisterParameter(parameter);
		}

		public void RegisterInput(IInput input)
		{
			_parentBuilder.RegisterInput(input);
		}

		public string Serialize()
			=> throw new NotImplementedException();

		public IQuerySubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType> As<TInterfaceType>()
			where TInterfaceType : class, IInterface
			=> throw new NotImplementedException();

		public IInterfaceSubBuilder<TBuilder, TInterface> WithField<TField>(
			Expression<Func<TInterface, TField>> fieldExpression)
			=> throw new NotImplementedException();
	}
}