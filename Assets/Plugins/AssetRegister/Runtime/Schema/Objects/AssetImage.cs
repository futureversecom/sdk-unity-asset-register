// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class AssetImage : NodeBase
	{
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("tokenId")] public string TokenId;
		[JsonProperty("url")] public string Url;
		[JsonProperty("version")] public float Version;
	}
	
	[JsonObject]	
	public sealed class AssetImageEdge : EdgeBase<AssetImage> { }
	[JsonObject]
	public sealed class AssetImagesConnection : ConnectionBase<AssetImageEdge> { }
}