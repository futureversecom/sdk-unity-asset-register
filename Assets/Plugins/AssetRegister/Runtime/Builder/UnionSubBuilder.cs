// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	internal class UnionSubBuilder<TBuilder, TUnion> : IUnionSubBuilder<TBuilder, TUnion>, ITokenProvider
		where TBuilder : IBuilder 
		where TUnion : class, IUnion
	{
		public string TokenString { get; }
		public List<IProvider> Children { get; } = new();
		
		private readonly TBuilder _parentBuilder;

		public UnionSubBuilder(TBuilder parentBuilder, string memberName)
		{
			TokenString = memberName;
			_parentBuilder = parentBuilder;
			
			// Typename required for deserialization
			Children.Add(new FieldToken("__typename"));
		}
		
		public TBuilder Done()
		{
			return _parentBuilder;
		}

		public IMemberSubBuilder<IUnionSubBuilder<TBuilder, TUnion>, TUnionType> On<TUnionType>()
			where TUnionType : class, TUnion
		{
			var memberString = $"... on {typeof(TUnionType).Name}";
			var builder = new MemberSubBuilder<UnionSubBuilder<TBuilder, TUnion>, TUnionType>(this, memberString);
			Children.Add(builder);
			return builder;
		}
		
		public IRequest Build()
		{
			return _parentBuilder.Build();
		}

		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await _parentBuilder.Execute(client, authToken, cancellationToken);
		}
	}
}