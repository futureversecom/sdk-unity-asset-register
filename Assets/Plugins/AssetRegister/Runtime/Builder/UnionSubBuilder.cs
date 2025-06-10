// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Text;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	public class UnionSubBuilder<TBuilder, TUnion> : IUnionSubBuilder<TBuilder, TUnion>, ITokenProvider
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
			Children.Add(new StringTokenProvider("__typename")); // Typename required for deserialization
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