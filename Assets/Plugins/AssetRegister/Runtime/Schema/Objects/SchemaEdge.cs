// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class SchemaEdge
	{
		[JsonProperty("cursor")] public string Cursor;
		[JsonProperty("node")] public Schema Node;
	}
}