// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class SSLCertificate : ISchema
	{
		[JsonProperty("cnameName")] public string CNameName;
		[JsonProperty("cnameValue")] public string CNameValue;
		[JsonProperty("status")] public string Status;
	}
}