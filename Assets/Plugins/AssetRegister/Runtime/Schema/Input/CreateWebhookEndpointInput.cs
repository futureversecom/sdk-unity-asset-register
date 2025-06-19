// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class CreateWebhookEndpointInput : IInput
	{
		[Float, JsonProperty("retries", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Retries;
		[String, Required, JsonProperty("url")]
		public string Url;

		public CreateWebhookEndpointInput(string url, float retries = default)
		{
			Retries = retries;
			Url = url;
		}
	}
}