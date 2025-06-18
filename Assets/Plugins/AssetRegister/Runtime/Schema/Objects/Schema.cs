// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Schema : NodeBase
	{
		[JsonProperty("name")] public string Name;
		[JsonProperty("namespace")] public string Namespace;
		[JsonProperty("schema")] public string SchemaString;
		[JsonProperty("version")] public int Version;
	}
	
	[JsonObject]
	public class SchemaEdge : EdgeBase<Schema> { }
	[JsonObject]
	public class SchemasConnection : ConnectionBase<SchemaEdge> { }
}