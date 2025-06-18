// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Domain : ISchema
	{
		[JsonProperty("id")] public string Id;
		[JsonProperty("url")] public string Url;
	}
	
	public sealed class DomainEdge : EdgeBase<Domain> { }
	public sealed class DomainsConnection : ConnectionBase<DomainEdge> { }
}