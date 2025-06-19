// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateWebhookEndpointInput : IInput
	{
		[Float, JsonProperty("retries")] public float Retries;
		[String, JsonProperty("url")] public string Url;
		[String, Required, JsonProperty("webhookId")]
		public string WebhookId;

		public UpdateWebhookEndpointInput(float retries, string url, string webhookId)
		{
			Retries = retries;
			Url = url;
			WebhookId = webhookId;
		}
	}
}