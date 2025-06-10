// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Core
{
	[JsonObject]
	internal class Error
	{
		[JsonProperty("message")] public string Message { get; private set; }
	}
}