// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Utils;
#if USING_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else
using System;
using System.Collections;
#endif

namespace AssetRegister.Runtime.Builder
{
	internal enum RequestType
	{
		Query,
		Mutation,
	}
	
	internal abstract class RequestBuilder<TBuilder> : IRequestBuilder<TBuilder> where TBuilder : class, IRequestBuilder<TBuilder>
	{
		protected readonly List<IProvider> Providers = new();
		private readonly Dictionary<string, string> _headers = new();

		protected RequestBuilder()
		{
			SetHeader("Content-Type", "application/json");
		}
		
		public IRequest Build()
		{
			var queryBody = new StringBuilder();
			var inputObject = new JObject();
			List<IParameter> parameters = new();
			foreach (var provider in Providers)
			{
				BuildQueryStringRecursive(provider, queryBody, parameters, inputObject, 1);
			}
			
			var query = new StringBuilder();
			var typeString = RequestType switch
			{
				RequestType.Query => "query",
				_ => "mutation",
			};
			query.Append($"{typeString} (");
			query.AppendJoin(", ", parameters.Select(p => $"${p.ParameterName}: {p.ParameterType}"));
			query.AppendLine(") {");
			query.Append(queryBody);
			query.Append("}");
			
			return new Request(query.ToString(), inputObject, _headers);
		}
		
#if USING_UNITASK
		public async UniTask<IResponse> 
#else 
		public IEnumerator
#endif
			Execute(
				IClient client,
#if USING_UNITASK
				CancellationToken cancellationToken = default
#else
				Action<IResponse> onComplete = null
#endif
			)
		{
			var request = Build();
#if USING_UNITASK
			return await client.SendRequest(request, cancellationToken);
#else
			yield return client.SendRequest(request, onComplete);
#endif
		}

		public TBuilder SetHeader(string headerName, string value)
		{
			_headers.Add(headerName, value);
			return this as TBuilder;
		}

		public TBuilder SetAuth(string authToken)
		{
			SetHeader("Authorization", authToken);
			return this as TBuilder;
		}

		protected abstract RequestType RequestType { get; }

		private static void BuildQueryStringRecursive(
			IProvider provider,
			StringBuilder queryBody,
			List<IParameter> parameters,
			JObject inputObject, 
			int depth)
		{
			var isToken = false;
			var hasChildren = provider.Children != null && provider.Children.Count > 0;
			
			if (provider is ITokenProvider tokenProvider && !string.IsNullOrEmpty(tokenProvider.TokenString))
			{
				isToken = true;
				queryBody.AppendLineIndented(
					hasChildren ? $"{tokenProvider.TokenString} {{" : tokenProvider.TokenString,
					depth
				);
			}
			if (provider is IParameterProvider parameterProvider && parameterProvider.Parameters != null)
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
					isToken ? depth + 1 : depth
				);
			}

			if (isToken)
			{
				queryBody.AppendLineIndented("}", depth);
			}
		}
	}
}