// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;
using UnityEngine;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class MutationRequestBuilder : RequestBuilder<IMutationData>
	{
		public MutationSubBuilder<TModel, TArgs> AddMutation<TModel, TArgs>(IMutation<TModel, TArgs> mutation)
			where TModel : class, IModel where TArgs : class, IArguments
		{
			return new MutationSubBuilder<TModel, TArgs>(this)
				.WithArgs(mutation.Arguments)
				.WithFunctionName(mutation.FunctionName);
		}
		
		public override Request Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("mutation (");
			var allParameters = RequestData.SelectMany(q => q.Parameters).ToList();
			queryString.Append(string.Join(", ", allParameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");
			
			var argsObject = new JObject();
			foreach (var mutationData in RequestData)
			{
				queryString.Append(mutationData.FunctionName);
				
				queryString.Append("(");
				queryString.Append(
					string.Join(",", mutationData.Parameters.Select(p => $"input: ${p.ParameterName}"))
				);
				queryString.Append(")");
				
				queryString.AppendLine("{");
				queryString.Append(BuildModelString(mutationData, false));
				queryString.AppendLine("}");
				
				argsObject.Merge(JObject.FromObject(mutationData.Args));
			}
			
			queryString.Append("}");
			Debug.Log(queryString.ToString());
			return new Request(queryString.ToString(), argsObject);
		}
	}
}