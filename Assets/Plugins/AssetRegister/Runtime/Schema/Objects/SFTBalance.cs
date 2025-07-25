// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public class SFTBalance : NodeBase
	{
		[JsonProperty("balance")] public float Balance;
		[JsonProperty("owner")] public Account Owner;
	}
}