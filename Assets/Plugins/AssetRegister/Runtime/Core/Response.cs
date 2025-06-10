// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Core
{
	[JsonObject]
	internal class Response : IResponse
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

			var name = Utils.GetSchemaName<T>();
			if (!_data.TryGetValue(name, out var token))
			{
				model = default(T);
				return false;
			}
			
			model = token.ToObject<T>();
			return model != null;
		}
	}
}