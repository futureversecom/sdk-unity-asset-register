// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Core
{
	[JsonObject]
	public class Response : IResponse
	{
		public bool Success => _data != null && string.IsNullOrEmpty(Error);
		public string Error { get; }
		
		private readonly JObject _data;
		
		internal Response(JObject data, string error = null)
		{
			_data = data;
			Error = error;
		}

		internal static Response Parse(string resultJson)
		{
			var json = JsonConvert.DeserializeObject<JObject>(resultJson);
			var data = json["data"] as JObject;
			var errors = json["errors"]?.ToObject<Error[]>();
			var error = errors != null ?
				$"GraphQL query returned errors: {string.Join(", ", errors.Select(e => e.Message))}" :
				null;
				
			return new Response(data, error);
		}

		public bool TryGetModel<T>(out T model) where T : class, IModel
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

		// This is a hacky way to do it. For mutations, the model object lives under the mutation object,
		// so it's not at the top level. That means we can't grab it off the top the same way we could do
		// for queries, so we do a breadth first search through the JSON hierarchy til we find our token.
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