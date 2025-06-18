// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class WebhookSubscription : NodeBase
	{
		[JsonProperty("actions")] public string[] Actions;
		[JsonProperty("collectionId")] public string CollectionId;
		[JsonProperty("createdAt")] public float CreatedAt;
		[JsonProperty("subscriptionId")] public string SubscriptionId;
		[JsonProperty("type")] public string Type;
		[JsonProperty("webhookId")] public string WebhookId;	
	}
	
	[JsonObject]
	public sealed class WebhookSubscriptionEdge : EdgeBase<WebhookSubscription> { }
	[JsonObject]
	public sealed class WebhookSubscriptionsConnection : ConnectionBase<WebhookSubscriptionEdge> { }
}