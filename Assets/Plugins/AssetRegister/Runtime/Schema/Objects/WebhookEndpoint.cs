// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class WebhookEndpoint : NodeBase
	{
		[JsonProperty("apiKey")] public string APIKey;
		[JsonProperty("createdAt")] public float CreatedAt;
		[JsonProperty("retries")] public float Retries;
		[JsonProperty("subscriber")] public string Subscriber;
		[JsonProperty("url")] public string Url;
		[JsonProperty("webhookId")] public string WebhookId;
	}
	
	[JsonObject]
	public sealed class WebhookEndpointEdge : EdgeBase<WebhookEndpoint> { }
	[JsonObject]
	public sealed class WebhookEndpointsConnection : ConnectionBase<WebhookEndpointEdge> { }
}