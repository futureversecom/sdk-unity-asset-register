// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Schemas;

namespace Plugins.AssetRegister.Runtime.Models
{
	/// <summary>
	/// 
	/// </summary>
	[JsonObject]
	public class Asset : IModel
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("assetTree")] public AssetTree AssetTree;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("assetType")] public AssetType AssetType;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("collection")] public Collection Collection;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("collectionId")] /*CollectionId*/ public string CollectionId;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id")] /*ID*/ public string Id;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("tokenId")] public string TokenId;
	}
}