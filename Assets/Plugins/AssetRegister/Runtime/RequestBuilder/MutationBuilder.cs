// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace AssetRegister.Runtime.RequestBuilder
{
	internal class MutationBuilder : IMutationBuilder
	{
		private readonly List<IMutationData> _mutationData = new();

		public IMutationBuilder RegisterData(IMutationData data)
		{
			_mutationData.Add(data);
			return this;
		}

		public IMutationSubBuilder<TModel, TArgs, IMutationBuilder, IMutationData> AddMutation<TModel, TArgs>(
			IMutation<TModel, TArgs> mutation) where TModel : class, IModel where TArgs : class, IArgs
		{
			return new MutationSubBuilder<TModel, TArgs, IMutationBuilder>(this).WithArgs(mutation.Arguments)
				.WithFunctionName(mutation.FunctionName);
		}
		
		public IRequest Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("mutation (");
			var allParameters = _mutationData.SelectMany(q => q.Parameters).ToList();
			queryString.Append(string.Join(", ", allParameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");
			
			var argsObject = new JObject();
			foreach (var mutationData in _mutationData)
			{
				queryString.Append(mutationData.FunctionName);
				
				queryString.Append("(");
				queryString.Append(
					string.Join(",", mutationData.Parameters.Select(p => $"input: ${p.ParameterName}"))
				);
				queryString.Append(")");
				
				queryString.AppendLine("{");
				queryString.Append(BuilderUtils.BuildModelString(mutationData, false));
				queryString.AppendLine("}");
				
				argsObject.Merge(JObject.FromObject(mutationData.Args));
			}
			
			queryString.Append("}");
			Debug.Log(queryString.ToString());
			return new Request(queryString.ToString(), argsObject);
		}
		
#if USING_UNITASK
		public async UniTask<IResponse> 
#else 
		public IEnumerator
#endif
			Execute(
				IClient client,
				string authenticationToken = null,
#if USING_UNITASK
				CancellationToken cancellationToken = default
#else
				Action<QueryResult> onComplete = null
#endif
			)
		{
			var queryObject = Build();
#if USING_UNITASK
			return await client.SendRequest(queryObject, authenticationToken, cancellationToken);
#else
			yield return client.Query(queryObject, authenticationToken, onComplete);
#endif
		}
	}
}