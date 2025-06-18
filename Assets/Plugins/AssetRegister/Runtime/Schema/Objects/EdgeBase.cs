// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public abstract class EdgeBase<T> : ISchema where T : class, ISchema
	{
		[JsonProperty("cursor")] public string Cursor;
		[JsonProperty("node")] public T Node;
	}
}