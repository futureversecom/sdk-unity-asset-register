// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Schemas
{
	/// <summary>
	/// 
	/// </summary>
	[JsonObject]
	public class Collection
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("chainId")] /*ChainID*/ public string ChainID;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("chainType")] /*ChainType*/ public string ChainType;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id")] /*ID*/ public string Id;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("location")] /*CollectionLocation*/ public string Location;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("name")] public string Name;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("schema")] public Schema Schema;
	}
}