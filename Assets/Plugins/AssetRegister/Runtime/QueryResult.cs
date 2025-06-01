// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime
{
	[JsonObject]
	public class QueryResult<T> where T : class, IModel
	{
		public bool Success => Data != null && string.IsNullOrEmpty(Error);
		
		public readonly T Data;
		public readonly string Error;
		
		public QueryResult(T data, string error = null)
		{
			Data = data;
			Error = error;
		}
	}
}