// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class TokenSchema : NodeBase
	{
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("schemaId")] public string SchemaId;
		[JsonProperty("tokenId")] public string TokenId;
		[JsonProperty("version")] public float Version;
	}
	
	[JsonObject]
	public sealed class TokenSchemaEdge : EdgeBase<TokenSchema> { }
	[JsonObject]
	public sealed class TokenSchemasConnection : ConnectionBase<TokenSchemaEdge> { }
}