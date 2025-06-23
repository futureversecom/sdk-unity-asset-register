// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class WebhookEndpointInput : IInput
	{
		[String, Required, JsonProperty("webhookId")]
		public string WebhookId;

		public WebhookEndpointInput(string webhookId)
		{
			WebhookId = webhookId;
		}
	}

	[JsonObject]
	public sealed class WebhookEndpointResult : IResult
	{
		[JsonProperty("webhookEndpoint")]
		public WebhookEndpoint WebhookEndpoint;
	}

	internal class WebhookEndpointQuery : IQuery<WebhookEndpoint, WebhookEndpointInput>
	{
		public string QueryName => "webhookEndpoint";
		public WebhookEndpointInput Input { get; }

		public WebhookEndpointQuery(string webhookId)
		{
			Input = new WebhookEndpointInput(webhookId);
		}
	}

}