// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegisterV2.Runtime
{
	internal class Request : IRequest
	{
		private readonly JObject _input;
		private readonly string _queryString;

		public Request(string queryString, JObject arguments)
		{
			_queryString = queryString;
			_input = arguments;
		}

		public override string ToString()
			=> _queryString;

		public string Serialize()
		{
			return JsonConvert.SerializeObject(
				new
				{
					query = _queryString,
					variables = _input,
				},
				Formatting.None
			);
		}
	}
}