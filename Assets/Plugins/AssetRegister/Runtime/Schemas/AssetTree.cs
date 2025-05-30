// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegister.Runtime.Schemas
{
	/// <summary>
	///
	/// </summary>
	[JsonObject]
	public class AssetTree
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("data")] public JObject Data;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id")] /*ID*/ public string Id;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("nodeId")] public string NodeId;
	}
}