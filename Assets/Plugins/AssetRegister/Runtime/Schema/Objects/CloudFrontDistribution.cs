// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class CloudFrontDistribution : ISchema
	{
		[JsonProperty("status")] public string Status;
		[JsonProperty("url")] public string Url;
	}
}