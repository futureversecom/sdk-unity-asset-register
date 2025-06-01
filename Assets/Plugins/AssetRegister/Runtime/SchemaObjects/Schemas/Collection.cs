// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class Collection
	{
		[JsonProperty("chainId")] public string ChainID;
		[JsonProperty("chainType")] public string ChainType;
		[JsonProperty("id")] public string Id;
		[JsonProperty("location")] public string Location;
		[JsonProperty("name")] public string Name;
		[JsonProperty("schema")] public Schema Schema;
	}
}