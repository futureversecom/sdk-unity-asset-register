// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class TokenSchemaInput : IInput
	{
		[String, JsonProperty("tokenId", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TokenId;
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;

		public TokenSchemaInput(string collectionId, string tokenId = default)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}
	}

	[JsonObject]
	public sealed class TokenSchemaResult : IResult
	{
		[JsonProperty("tokenSchema")]
		public TokenSchema TokenSchema;
	}

	internal class TokenSchemaQuery : IQuery<TokenSchema, TokenSchemaInput>
	{
		public TokenSchemaInput Input { get; }

		public TokenSchemaQuery(string collectionId, string tokenId = default)
		{
			Input = new TokenSchemaInput(collectionId, tokenId);
		}
	}

}