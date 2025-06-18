// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject("namespace")]
	public sealed class Namespace : NodeBase
	{
		[JsonProperty("schemas")] public SchemasConnection Schemas;
		[JsonProperty("url")] public string Url;
	}
	
	[JsonObject]
	public sealed class NamespaceEdge : EdgeBase<Namespace> { }
	[JsonObject]
	public sealed class NamespacesConnection : ConnectionBase<NamespaceEdge> { }
}