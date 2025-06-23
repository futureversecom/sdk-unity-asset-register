// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

		public bool TryGetModel<TResult>(out TResult result) where TResult : class, IResult
		{
			if (_data == null)
			{
				result = default(TResult);
				return false;
			}
			
			result = _data.ToObject<TResult>();
			return result != null;
		}
	}
}