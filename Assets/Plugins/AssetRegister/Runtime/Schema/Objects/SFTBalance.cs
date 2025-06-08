// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public class SFTBalance
	{
		[JsonProperty("balance")] public float Balance;
		[JsonProperty("id")] public string Id;
		[JsonProperty("owner")] public Account Owner;
	}
}