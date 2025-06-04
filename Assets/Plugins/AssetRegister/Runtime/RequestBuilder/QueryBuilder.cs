// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;
using UnityEngine;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class QueryBuilder : IQueryBuilder
	{
		private readonly List<IQueryData> _queryData = new();
		
		public IQueryBuilder RegisterData(IQueryData data)
		{
			_queryData.Add(data);
			return this;
		}

		public IQuerySubBuilder<TModel, TArgs, IQueryBuilder, IQueryData> AddQuery<TModel, TArgs>(IQuery<TModel, TArgs> query)
			where TModel : class, IModel where TArgs : class, IArguments
		{
			return new QuerySubBuilder<TModel, TArgs, IQueryBuilder>(this).WithArgs(query.Arguments);
		}
		
		public Request Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("query (");
			var allParameters = _queryData.SelectMany(q => q.Parameters).ToList();
			queryString.Append(string.Join(", ", allParameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");

			var argsObject = new JObject();
			foreach (var queryData in _queryData)
			{
				queryString.Append(BuilderUtils.BuildModelString(queryData, true));
				argsObject.Merge(JObject.FromObject(queryData.Args));
			}
			
			queryString.Append("}");
			Debug.Log(queryString.ToString());
			return new Request(queryString.ToString(), argsObject);
		}
		
#if USING_UNITASK
		public async UniTask<Result> 
#else 
		public IEnumerator
#endif
			Execute(
				IAssetRegisterClient client,
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
			return await client.MakeRequest(queryObject, authenticationToken, cancellationToken);
#else
			yield return client.Query(queryObject, authenticationToken, onComplete);
#endif
		}
	}
}