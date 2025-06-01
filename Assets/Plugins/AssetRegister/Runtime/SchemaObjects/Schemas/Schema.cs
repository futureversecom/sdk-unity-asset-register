// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class Schema
	{
		[JsonProperty("id")] public string Id;
		[JsonProperty("name")] public string Name;
		[JsonProperty("namespace")] public string Namespace;
		[JsonProperty("schema")] public string SchemaString;
		[JsonProperty("version")] public int Version;
	}
}