// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class AssetTree : NodeBase
	{
		[JsonProperty("data")] public JObject Data;
		[JsonProperty("nodeId")] public string NodeId;
	}
}