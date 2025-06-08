// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public class Account
	{
		[JsonProperty("handle")] public string Handle;
		[JsonProperty("id")] public string Id;
	}
}