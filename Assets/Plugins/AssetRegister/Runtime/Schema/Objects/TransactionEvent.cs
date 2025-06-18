// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class TransactionEvent : ISchema
	{
		[JsonProperty("action")] public string Action;
		[JsonProperty("args")] public string[] Args;
		[JsonProperty("type")] public string Type;
	}
}