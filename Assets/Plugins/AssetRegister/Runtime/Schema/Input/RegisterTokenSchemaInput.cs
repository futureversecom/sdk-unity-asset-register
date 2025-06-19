// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RegisterTokenSchemaInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[SchemaIdentifier, Required, JsonProperty("schemaId")]
		public string SchemaId;
		[String, JsonProperty("tokenId")] public string TokenId;

		public RegisterTokenSchemaInput(string collectionId, string schemaId, string tokenId)
		{
			CollectionId = collectionId;
			SchemaId = schemaId;
			TokenId = tokenId;
		}
	}
}