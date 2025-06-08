// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Text;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	public class UnionSubBuilder<TBuilder, TUnion> : IUnionSubBuilder<TBuilder, TUnion>, IQueryAssembler, IToken 
		where TBuilder : IBuilder, IQueryAssembler 
		where TUnion : class, IUnion
	{
		private readonly TBuilder _parentBuilder;
		private readonly StringBuilder _stringBuilder = new();
		private string _storedSubclassName;

		public UnionSubBuilder(TBuilder parentBuilder)
		{
			_parentBuilder = parentBuilder;
		}
		
		public TBuilder Done()
		{
			_parentBuilder.RegisterToken(this);
			return _parentBuilder;
		}

		public IQuerySubBuilder<IUnionSubBuilder<TBuilder, TUnion>, TUnionType> As<TUnionType>()
			where TUnionType : class, TUnion
		{
			_storedSubclassName = typeof(TUnionType).Name;
			return new QuerySubBuilder<UnionSubBuilder<TBuilder, TUnion>, TUnionType>(this);
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
			_stringBuilder.AppendLine($"... on {_storedSubclassName} {{");
			_stringBuilder.AppendLine("__typename"); // Must include typename for unions for deserialization purposes
			_stringBuilder.Append(token.Serialize());
			_stringBuilder.Append("}");
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
			=> _stringBuilder.ToString();
	}
}