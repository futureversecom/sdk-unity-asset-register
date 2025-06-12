// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Core
{
	internal class Request : IRequest
	{
		public Dictionary<string, string> Headers { get; }
		
		private readonly JObject _input;
		private readonly string _queryBody;

		public Request(string queryBody, JObject arguments, Dictionary<string, string> headers)
		{
			_queryBody = queryBody;
			_input = arguments;
			Headers = headers;
		}

		public void OverrideInputs<TInput>(TInput input) where TInput : class, IInput
		{
			_input.Merge(JObject.FromObject(input));
		}

		public override string ToString()
			=> _queryBody;

		public string Serialize()
		{
			return JsonConvert.SerializeObject(
				new
				{
					query = _queryBody,
					variables = _input,
				},
				Formatting.None
			);
		}
	}
}