// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;
#if USING_UNITASK && !AR_SDK_NO_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else
using System;
using System.Collections;
#endif

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

#if USING_UNITASK && !AR_SDK_NO_UNITASK
		public async UniTask<IResponse> Execute(
			IClient client,
			CancellationToken cancellationToken = default)
		{
			return await _parentBuilder.Execute(client, cancellationToken);
		}
#else
		public IEnumerator Execute(IClient client, Action<IResponse> callback)
		{
			return _parentBuilder.Execute(client, callback);
		}
#endif
	}
}