// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class AssetOwner : NodeBase
	{
		[JsonProperty("asset")] public Asset Asset;
		[JsonProperty("balance")] public int Balance;
		[JsonProperty("owner")] public string Owner;
	}
	
	[JsonObject]
	public sealed class AssetOwnerEdge : EdgeBase<AssetOwner> { }
	[JsonObject]
	public sealed class AssetOwnersConnection : ConnectionBase<AssetOwnerEdge> { }
}