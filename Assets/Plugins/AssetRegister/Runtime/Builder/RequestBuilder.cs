// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Utils;
using UnityEngine;

namespace AssetRegister.Runtime.Builder
{
	internal enum RequestType
	{
		Query,
		Mutation,
	}
	
	internal abstract class RequestBuilder
	{
		protected readonly List<IProvider> Providers = new();
		
		protected abstract RequestType RequestType { get; }
		
		protected Request BuildRequest()
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
				queryBody.AppendLineIndented(
					hasChildren ? $"{tokenProvider.TokenString} {{" : tokenProvider.TokenString,
					depth
				);
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
	}
}