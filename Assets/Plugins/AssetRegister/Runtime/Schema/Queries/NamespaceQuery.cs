// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class NamespaceInput : IInput
	{
		[Url, Required, JsonProperty("namespace")]
		public string Namespace;

		public NamespaceInput(string @namespace)
		{
			Namespace = @namespace;
		}
	}
	
	[JsonObject]
	public sealed class NamespaceResult : IResult
	{
		[JsonProperty("namespace")] public string Namespace;
	}
	
	internal class NamespaceQuery : IQuery<Namespace, NamespaceInput>
	{
		public string QueryName => "namespace";
		public NamespaceInput Input { get; }

		public NamespaceQuery(string @namespace)
		{
			Input = new NamespaceInput(@namespace);
		}
	}
}