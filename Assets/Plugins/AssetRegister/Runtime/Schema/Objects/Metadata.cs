// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Metadata : NodeBase
	{
		[JsonProperty("attributes")] public JToken Attributes;
		[JsonProperty("image")] public string Image;
		[JsonProperty("properties")] public JToken Properties;
		[JsonProperty("rawAttributes")] public JToken RawAttributes;
		[JsonProperty("uri")] public string Uri;
	}
}