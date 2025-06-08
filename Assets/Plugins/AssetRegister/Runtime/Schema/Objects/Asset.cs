// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Schema.Enums;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Unions;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject("asset")]
	public sealed class Asset : IModel
	{
		[JsonProperty("assetTree")] public AssetTree AssetTree;
		[JsonProperty("assetType")] public AssetType AssetType;
		[JsonProperty("collection")] public Collection Collection;
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("id")] public string Id;
		[JsonProperty("tokenId")] public string TokenId;
		[JsonProperty("name")] public string Name;
		[JsonProperty("ownership"), JsonConverter(typeof(UnionConverter))] 
		public AssetOwnership Ownership;
	}
}