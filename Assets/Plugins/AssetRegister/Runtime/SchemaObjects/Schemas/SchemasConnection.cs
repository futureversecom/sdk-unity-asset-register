// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class SchemasConnection
	{
		[JsonProperty("edges")] public SchemaEdge[] Edges;
		[JsonProperty("pageInfo")] public PageInfo PageInfo;
		[JsonProperty("total")] public float Total;
	}
}