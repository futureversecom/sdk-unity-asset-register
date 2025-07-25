// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Collection : NodeBase
	{
		[JsonProperty("chainId")] public string ChainID;
		[JsonProperty("chainType")] public string ChainType;
		[JsonProperty("location")] public string Location;
		[JsonProperty("name")] public string Name;
		[JsonProperty("schema")] public Schema Schema;
	}
	
	[JsonObject]
	public sealed class CollectionEdge : EdgeBase<Collection> { }
	[JsonObject]
	public sealed class CollectionConnection : ConnectionBase<CollectionEdge> { }
}