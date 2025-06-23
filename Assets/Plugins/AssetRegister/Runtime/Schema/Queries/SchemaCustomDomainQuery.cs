// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class SchemaCustomDomainInput : IInput
	{
		[String, Required, JsonProperty("domainName")]
		public string DomainName;

		public SchemaCustomDomainInput(string domainName)
		{
			DomainName = domainName;
		}
	}

	[JsonObject]
	public sealed class SchemaCustomDomainResult : IResult
	{
		[JsonProperty("schemaCustomDomain")]
		public SchemaCustomDomain SchemaCustomDomain;
	}

	internal class SchemaCustomDomainQuery : IQuery<SchemaCustomDomain, SchemaCustomDomainInput>
	{
		public string QueryName => "schemaCustomDomain";
		public SchemaCustomDomainInput Input { get; }

		public SchemaCustomDomainQuery(string domainName)
		{
			Input = new SchemaCustomDomainInput(domainName);
		}
	}

}