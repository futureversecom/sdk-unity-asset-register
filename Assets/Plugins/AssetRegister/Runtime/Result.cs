// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Models;

namespace Plugins.AssetRegister.Runtime
{
	[JsonObject]
	public class Result<T> where T : class, IModel
	{
		public bool Success => Data != null && string.IsNullOrEmpty(Error);
		
		public readonly T Data;
		public readonly string Error;
		
		public Result(T data, string error = null)
		{
			Data = data;
			Error = error;
		}
	}
}