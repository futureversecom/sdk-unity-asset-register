// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime
{
	[JsonObject]
	public class QueryResult
	{
		public bool Success => _data != null && string.IsNullOrEmpty(Error);
		
		private readonly JObject _data;
		public readonly string Error;
		
		internal QueryResult(JObject data, string error = null)
		{
			_data = data;
			Error = error;
		}

		public bool TryRetrieveModel<T>(out T model) where T : class, IModel
		{
			if (_data == null)
			{
				model = default(T);
				return false;
			}

			var type = typeof(T);
			var modelAttribute = type.GetCustomAttribute<GraphQLModelAttribute>();
			if (modelAttribute == null)
			{
				model = default(T);
				return false;
			}

			if (!_data.TryGetValue(modelAttribute.ResponseName, out var token))
			{
				model = default(T);
				return false;
			}

			model = token.ToObject<T>();
			return model != null;
		}
	}
}