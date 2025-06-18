// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class RNS : NodeBase
	{
		[JsonProperty("asset")] public Asset Asset;
		[JsonProperty("name")] public string Name;
	}
}