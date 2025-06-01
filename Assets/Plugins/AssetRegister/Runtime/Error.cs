// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegister.Runtime
{
	[JsonObject]
	public class Error
	{
		[JsonProperty("message")] public string Message { get; private set; }
	}
}