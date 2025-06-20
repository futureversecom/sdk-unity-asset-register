// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class WebhookSubscriptionInput : IInput
	{
		[String, Required, JsonProperty("subscriptionId")]
		public string SubscriptionId;

		public WebhookSubscriptionInput(string subscriptionId)
		{
			SubscriptionId = subscriptionId;
		}
	}

	[JsonObject]
	public sealed class WebhookSubscriptionResult : IResult
	{
		[JsonProperty("webhookSubscription")]
		public WebhookSubscription WebhookSubscription;
	}

	internal class WebhookSubscriptionQuery : IQuery<WebhookSubscription, WebhookSubscriptionInput>
	{
		public WebhookSubscriptionInput Input { get; }

		public WebhookSubscriptionQuery(string subscriptionId)
		{
			Input = new WebhookSubscriptionInput(subscriptionId);
		}
	}

}