// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class SchemaCustomDomain : NodeBase
	{
		[JsonProperty("cloudfront")] public CloudFrontDistribution Cloudfront;
		[JsonProperty("createdBy")] public string CreatedBy;
		[JsonProperty("domainName")] public string DomainName;
		[JsonProperty("sslCertificate")] public SSLCertificate SSLCertificate;
	}
}