// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Objects.Enums;
using AssetRegister.Runtime.Objects.Schemas;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Objects.Models
{
	[JsonObject, GraphQLModel("asset")]
	public sealed class AssetModel : IModel
	{
		[JsonProperty("assetTree")] public AssetTree AssetTree;
		[JsonProperty("assetType")] public AssetType AssetType;
		[JsonProperty("collection")] public Collection Collection;
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("id")] public string Id;
		[JsonProperty("tokenId")] public string TokenId;
	}
}