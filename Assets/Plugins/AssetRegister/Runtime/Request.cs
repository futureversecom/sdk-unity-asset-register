// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class Request
	{
		public JObject Args { get; private set; }
		public readonly string QueryString;

		public Request(string queryString, JObject arguments)
		{
			QueryString = queryString;
			Args = arguments;
		}

		public void OverrideArguments<T>(T arguments) where T : class, IArguments
		{
			Args.Merge(JObject.FromObject(arguments));
		}

		public override string ToString()
			=> QueryString;
	}
}