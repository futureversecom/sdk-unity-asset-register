// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class SchemaEdge
	{
		[JsonProperty("cursor")] public string Cursor;
		[JsonProperty("node")] public Schema Node;
	}
}