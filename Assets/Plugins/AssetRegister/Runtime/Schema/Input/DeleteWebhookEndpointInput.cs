// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class DeleteWebhookEndpointInput : IInput
	{
		[String, Required, JsonProperty("webhookId")]
		public string WebhookId;

		public DeleteWebhookEndpointInput(string webhookId)
		{
			WebhookId = webhookId;
		}
	}
}