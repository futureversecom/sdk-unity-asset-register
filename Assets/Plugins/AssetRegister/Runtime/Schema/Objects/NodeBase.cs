// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Schema.Interfaces;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public class NodeBase : INode
	{
		[JsonProperty("id")] public string Id { get; protected set; }
	}
}