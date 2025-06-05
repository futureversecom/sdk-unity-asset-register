// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Text;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Collections.Generic;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System;
using System.Collections;
#endif

namespace AssetRegister.Runtime.RequestBuilder
{
	internal class QueryBuilder : IQueryBuilder
	{
		private readonly List<IQueryData> _queryData = new();
		
		public IQueryBuilder RegisterData(IQueryData data)
		{
			_queryData.Add(data);
			return this;
		}

		public IQuerySubBuilder<TModel, TInput, IQueryBuilder, IQueryData> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : class, IModel where TInput : class, IInput
		{
			return new QuerySubBuilder<TModel, TInput, IQueryBuilder>(this).WithInput(query.Arguments);
		}
		
		public IRequest Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("query (");
			var allParameters = _queryData.SelectMany(q => q.Parameters).ToList();
			queryString.Append(string.Join(", ", allParameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");

			var inputObject = new JObject();
			foreach (var queryData in _queryData)
			{
				queryString.Append(BuilderUtils.BuildModelString(queryData, true));
				inputObject.Merge(JObject.FromObject(queryData.Input));
			}
			
			queryString.Append("}");
			Debug.Log(queryString.ToString());
			return new Request(queryString.ToString(), inputObject);
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
				Action<IResponse> onComplete = null
#endif
			)
		{
			var request = Build();
#if USING_UNITASK
			return await client.SendRequest(request, authenticationToken, cancellationToken);
#else
			yield return client.SendRequest(request, authenticationToken, onComplete);
#endif
		}
	}
}