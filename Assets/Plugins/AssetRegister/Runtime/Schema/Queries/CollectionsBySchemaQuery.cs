// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class CollectionsBySchemaInput : IInput
	{
		[SchemaIdentifier, Required, JsonProperty("schemaId")]
		public string SchemaId;

		public CollectionsBySchemaInput(string schemaId)
		{
			SchemaId = schemaId;
		}
	}

	[JsonObject]
	public sealed class CollectionsBySchemaResult : IResult
	{
		[JsonProperty("collectionsBySchema")]
		public Collection[] CollectionsBySchema;
	}

	internal class CollectionsBySchemaQuery : IQuery<Collection, CollectionsBySchemaInput>
	{
		public CollectionsBySchemaInput Input { get; }

		public CollectionsBySchemaQuery(string schemaId)
		{
			Input = new CollectionsBySchemaInput(schemaId);
		}
	}
}