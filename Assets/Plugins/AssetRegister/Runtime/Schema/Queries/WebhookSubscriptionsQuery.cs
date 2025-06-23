// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class WebhookSubscriptionsInput : IInput
	{
		[String, Required, JsonProperty("webhookId")]
		public string WebhookId;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public WebhookSubscriptionsInput(
			string webhookId,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			WebhookId = webhookId;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class WebhookSubscriptionsResult : IResult
	{
		[JsonProperty("webhookSubscriptions")]
		public WebhookSubscriptionsConnection WebhookSubscriptions;
	}

	internal class WebhookSubscriptionsQuery : IQuery<WebhookSubscriptionsConnection, WebhookSubscriptionsInput>
	{
		public string QueryName => "webhookSubscriptions";
		public WebhookSubscriptionsInput Input { get; }

		public WebhookSubscriptionsQuery(
			string webhookId,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new WebhookSubscriptionsInput(webhookId, before, after, first, last);
		}
	}

}