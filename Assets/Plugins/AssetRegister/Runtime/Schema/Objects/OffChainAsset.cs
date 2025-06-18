// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class OffChainAsset : NodeBase
	{
		[JsonProperty("assetId")] public string AssetId;
		[JsonProperty("creatorCollectionId")] public string CreatorCollectionId;
		[JsonProperty("creatorId")] public string CreatorId;
		[JsonProperty("tokenId")] public string TokenId;
		[JsonProperty("type")] public string Type;
	}
	
	[JsonObject]
	public sealed class OffChainAssetEdge : EdgeBase<OffChainAsset> { }
	[JsonObject]
	public sealed class OffChainAssetsConnection : ConnectionBase<OffChainAssetEdge> { }
}