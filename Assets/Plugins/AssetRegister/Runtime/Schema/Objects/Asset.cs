// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Enums;
using AssetRegister.Runtime.Schema.Unions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject("asset")]
	public sealed class Asset : NodeBase
	{
		public sealed class ProfilesInput : IInput
		{
			[String, Required, JsonProperty("key")] public string Key;

			public ProfilesInput(string key)
			{
				Key = key;
			}
		}
		
		[JsonProperty("assetTree")] public AssetTree AssetTree;
		[JsonProperty("assetType")] public AssetType AssetType;
		[JsonProperty("collection")] public Collection Collection;
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("links"), JsonConverter(typeof(UnionConverter))] 
		public AssetLink Links;
		[JsonProperty("metadata")] public Metadata Metadata;
		[JsonProperty("ownership"), JsonConverter(typeof(UnionConverter))] 
		public AssetOwnership Ownership;
		[JsonProperty("profiles")] public JObject Profiles;
		[JsonProperty("schema")] public Schema Schema;
		[JsonProperty("tokenId")] public string TokenId;
	}
	
	[JsonObject]
	public sealed class AssetEdge : EdgeBase<Asset> { }
	[JsonObject]
	public sealed class AssetConnection : ConnectionBase<AssetEdge> { }
}