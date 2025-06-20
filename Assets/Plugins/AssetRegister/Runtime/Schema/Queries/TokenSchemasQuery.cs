// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class TokenSchemasInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public TokenSchemasInput(
			string collectionId,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			CollectionId = collectionId;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class TokenSchemasResult : IResult
	{
		[JsonProperty("tokenSchemas")]
		public TokenSchemasConnection TokenSchemas;
	}

	internal class TokenSchemasQuery : IQuery<TokenSchemasConnection, TokenSchemasInput>
	{
		public TokenSchemasInput Input { get; }

		public TokenSchemasQuery(
			string collectionId,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new TokenSchemasInput(collectionId, before, after, first, last);
		}
	}

}