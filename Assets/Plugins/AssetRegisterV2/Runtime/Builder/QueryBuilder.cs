// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegisterV2.Runtime.Builder;
using UnityEngine;

namespace Plugins.AssetRegisterV2.Runtime
{
	public class QueryBuilder : IQueryBuilder, IQueryAssembler
	{
		private readonly List<IToken> _tokens = new();
		private readonly List<IParameter> _parameters = new();

		public IRequest Build()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("query (");
			stringBuilder.Append(string.Join(", ", _parameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			stringBuilder.AppendLine(") {");
			foreach (var token in _tokens)
			{
				stringBuilder.Append(token.Serialize());
			}
			stringBuilder.Append("}");
			var queryString = stringBuilder.ToString();
			Debug.Log(queryString);
			
			return new Request(queryString, null);
		}

		public UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default)
			=> throw new System.NotImplementedException();

		public IQuerySubBuilder<IQueryBuilder, TModel> Add<TModel>()
			=> new QuerySubBuilder<QueryBuilder, TModel>(this);

		public void RegisterToken(IToken token)
		{
			_tokens.Add(token);
		}

		public void RegisterParameter(IParameter parameter)
		{
			_parameters.Add(parameter);
		}
	}
}