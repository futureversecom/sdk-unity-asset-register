// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;
using UnityEngine;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class QueryRequestBuilder : RequestBuilder<IQueryData>
	{
		public QuerySubBuilder<TModel, TArgs> AddQuery<TModel, TArgs>(IQuery<TModel, TArgs> query)
			where TModel : class, IModel where TArgs : class, IArguments
		{
			return new QuerySubBuilder<TModel, TArgs>(this).SetArgs(query.Arguments);
		}
		
		public override GraphQLRequest Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("query (");
			var allParameters = RequestData.SelectMany(q => q.Parameters).ToList();
			queryString.Append(string.Join(", ", allParameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");

			var argsObject = new JObject();
			foreach (var queryData in RequestData)
			{
				queryString.Append(BuildModelString(queryData, true));
				argsObject.Merge(JObject.FromObject(queryData.Args));
			}
			
			queryString.Append("}");
			Debug.Log(queryString.ToString());
			return new GraphQLRequest(queryString.ToString(), argsObject);
		}
	}
}