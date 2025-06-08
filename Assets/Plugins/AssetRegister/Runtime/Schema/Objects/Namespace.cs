// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject("namespace")]
	public sealed class Namespace : IModel
	{
		[JsonProperty("id")] public string Id;
		[JsonProperty("schemas")] public SchemasConnection Schemas;
		[JsonProperty("url")] public string Url;
	}
}