// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class CreateSchemaInput : IInput
	{
		[String, Required, JsonProperty("name")] public string Name;
		[Url, Required, JsonProperty("namespace")] public string Namespace;
		[String, Required, JsonProperty("schema")] public string Schema;
		[Float, Required, JsonProperty("version")] public float Version;
	}
}