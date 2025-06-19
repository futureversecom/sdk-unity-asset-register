// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RegisterSchemaCustomDomainInput : IInput
	{
		[String, Required, JsonProperty("domainName")]
		public string DomainName;

		public RegisterSchemaCustomDomainInput(string domainName)
		{
			DomainName = domainName;
		}
	}
}