// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegister.Runtime
{
	public static class AssetRegister
	{
		public static IQueryBuilder NewQuery()
		{
			return new QueryBuilder();
		}
		
		public static IMutationBuilder NewMutation()
		{
			return new MutationBuilder();
		}

		public static IRequest RawRequest(string body, object variables = null, Dictionary<string, string> headers = null)
		{
			var headerDict = headers ?? new Dictionary<string, string>();
			headerDict.Add("Content-Type", "application/json");
			return new Request(
				body,
				variables == null ? new JObject() : JObject.FromObject(variables),
				headerDict
			);
		}
	}
}