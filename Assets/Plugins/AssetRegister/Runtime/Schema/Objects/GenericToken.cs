// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class GenericToken : NodeBase
	{
		[JsonProperty("chainId")] public string ChainId;
		[JsonProperty("chainType")] public string ChainType;
		[JsonProperty("decimals")] public float Decimals;
		[JsonProperty("issuer")] public string Issuer;
		[JsonProperty("location")] public string Location;
		[JsonProperty("name")] public string Name;
		[JsonProperty("symbol")] public string Symbol;
	}
}