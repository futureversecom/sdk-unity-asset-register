// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject, GraphQLModel("asset")]
	public class AssetModel : IModel
	{
		[JsonProperty("assetTree")] public AssetTree AssetTree;
		[JsonProperty("assetType")] public AssetType AssetType;
		[JsonProperty("collection")] public Collection Collection;
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("id")] public string Id;
		[JsonProperty("tokenId")] public string TokenId;
	}
}