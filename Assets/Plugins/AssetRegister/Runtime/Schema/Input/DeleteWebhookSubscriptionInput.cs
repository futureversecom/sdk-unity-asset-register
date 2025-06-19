// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class DeleteWebhookSubscriptionInput : IInput
	{
		[String, Required, JsonProperty("subscriptionId")]
		public string SubscriptionId;
		[String, Required, JsonProperty("webhookId")]
		public string WebhookId;

		public DeleteWebhookSubscriptionInput(string subscriptionId, string webhookId)
		{
			SubscriptionId = subscriptionId;
			WebhookId = webhookId;
		}
	}
}