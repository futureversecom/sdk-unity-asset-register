// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime
{
	public class QueryObject<TModel, TVariables> 
		where TModel: class, IModel 
		where TVariables : class, IQueryVariables
	{
		public TVariables Variables { get; private set; }
		
		public readonly string QueryName;
		public readonly string QueryString;

		public QueryObject(string queryName, string queryString, TVariables variables)
		{
			QueryName = queryName;
			QueryString = queryString;
			Variables = variables;
		}

		public void SetVariables(TVariables variables)
		{
			Variables = variables;
		}

		public override string ToString()
			=> QueryString;
	}
}