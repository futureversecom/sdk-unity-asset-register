// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Objects.Schemas
{
	[JsonObject]
	public sealed class AssetTree
	{
		[JsonProperty("data")] public JObject Data;
		[JsonProperty("id")] public string Id;
		[JsonProperty("nodeId")] public string NodeId;
	}
}