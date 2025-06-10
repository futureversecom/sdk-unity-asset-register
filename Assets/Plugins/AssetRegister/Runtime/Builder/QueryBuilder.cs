// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Utils;
using UnityEngine;

namespace AssetRegister.Runtime.Builder
{
	public class ParameterInfo : IParameter
	{
		public string ParameterName { get; }
		public string ParameterType { get; }

		public ParameterInfo(string parameterName, string parameterType)
		{
			ParameterName = parameterName;
			ParameterType = parameterType;
		}
	}
	
	public class QueryBuilder : IQueryBuilder
	{
		private readonly List<IProvider> _providers = new();

		public IRequest Build()
		{
			var queryBody = new StringBuilder();
			var inputObject = new JObject();
			List<IParameter> parameters = new();
			foreach (var provider in _providers)
			{
				BuildQueryStringRecursive(provider, queryBody, parameters, inputObject, 1);
			}

			var query = new StringBuilder();
			query.Append("query (");
			query.AppendJoin(", ", parameters.Select(p => $"${p.ParameterName}: {p.ParameterType}"));
			query.AppendLine(") {");
			query.Append(queryBody);
			query.Append("}");
			
			var queryString = query.ToString();
			Debug.Log(queryString);
			
			return new Request(queryString, inputObject);
		}

		private static void BuildQueryStringRecursive(
			IProvider provider,
			StringBuilder queryBody,
			List<IParameter> parameters,
			JObject inputObject, 
			int depth)
		{
			var isToken = false;
			var hasChildren = provider.Children != null && provider.Children.Count > 0;
			
			if (provider is ITokenProvider tokenProvider)
			{
				isToken = true;
				if (hasChildren)
				{
					queryBody.AppendLineIndented($"{tokenProvider.TokenString} {{", depth);
				}
				else
				{
					queryBody.AppendLineIndented(tokenProvider.TokenString, depth);
				}
			}
			if (provider is IParameterProvider parameterProvider)
			{
				foreach (var param in parameterProvider.Parameters)
				{
					parameters.Add(param);
				}
			}
			if (provider is IInputProvider inputProvider && inputProvider.Input != null)
			{
				inputObject.Merge(JObject.FromObject(inputProvider.Input));
			}

			if (!hasChildren)
			{
				return;
			}

			foreach (var child in provider.Children)
			{
				BuildQueryStringRecursive(
					child,
					queryBody,
					parameters,
					inputObject,
					depth + 1
				);
			}

			if (isToken)
			{
				queryBody.AppendLineIndented("}", depth);
			}
		}

		public UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default)
			=> throw new System.NotImplementedException();

		public IMemberSubBuilder<IQueryBuilder, TModel> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : IModel where TInput : class, IInput
		{
			var builder = new QuerySubBuilder<QueryBuilder, TModel, TInput>(this, query);
			_providers.Add(builder);
			return builder;
		}
	}
}