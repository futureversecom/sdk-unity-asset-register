// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class GenericTokenBalance : NodeBase
	{
		[JsonProperty("amount")] public string Amount;
		[JsonProperty("genericToken")] public GenericToken GenericToken;
		[JsonProperty("owner")] public Account Owner;
	}
}