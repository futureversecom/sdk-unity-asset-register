// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject("namespace")]
	public sealed class Namespace : NodeBase
	{
		// ReSharper disable once InconsistentNaming
		public SchemasConnection schemas(
			[String] string before,
			[String] string after,
			[Float] float first,
			[Float] float last)
		{
			return Schemas;
		}
		
		[JsonProperty("schemas")] public SchemasConnection Schemas;
		[JsonProperty("url")] public string Url;
	}
	
	[JsonObject]
	public sealed class NamespaceEdge : EdgeBase<Namespace> { }
	[JsonObject]
	public sealed class NamespacesConnection : ConnectionBase<NamespaceEdge> { }
}