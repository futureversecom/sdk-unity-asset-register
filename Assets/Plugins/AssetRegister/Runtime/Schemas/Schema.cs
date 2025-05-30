// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Schemas
{
	/// <summary>
	/// 
	/// </summary>
	[JsonObject]
	public class Schema
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id")] /*ID*/ public string Id;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("name")] public string Name;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("namespace")] /*Url*/ public string Namespace;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("schema")] public string SchemaString;
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("version")] public int Version;
	}
}