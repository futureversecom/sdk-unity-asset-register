// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class SchemasConnection
	{
		[JsonProperty("edges")] public SchemaEdge[] Edges;
		[JsonProperty("pageInfo")] public PageInfo PageInfo;
		[JsonProperty("total")] public float Total;
	}
}