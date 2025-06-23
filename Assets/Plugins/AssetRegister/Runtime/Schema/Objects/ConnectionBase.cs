// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public abstract class ConnectionBase<T> : ISchema where T : ISchema
	{
		[JsonProperty("edges")] public T[] Edges;
		[JsonProperty("pageInfo")] public PageInfo PageInfo;
		[JsonProperty("total")] public float Total;
	}
}