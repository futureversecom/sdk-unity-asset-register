// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime
{
	[JsonObject]
	public class Result
	{
		public bool Success => _data != null && string.IsNullOrEmpty(Error);
		
		private readonly JObject _data;
		public readonly string Error;
		
		internal Result(JObject data, string error = null)
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

			var token = FindToken(modelAttribute.ResponseName);
			if (token == null)
			{
				model = default(T);
				return false;
			}
			
			model = token.ToObject<T>();
			return model != null;
		}

		private JToken FindToken(string tokenName)
		{
			var queue = new Queue<JToken>();
			queue.Enqueue(_data);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (current is not JObject obj)
				{
					continue;
				}

				foreach (var property in obj.Properties())
				{
					if (property.Name == tokenName)
					{
						return property.Value;
					}
					
					queue.Enqueue(property.Value);
				}
			}

			return null;
		}
	}
}