// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class CreateWebhookSubscriptionInput : IInput
	{
		[String, Required, JsonProperty("actions")] public string[] Actions;
		[CollectionId, Required, JsonProperty("collectionId")] public string CollectionId;
		[String, Required, JsonProperty("type")] public string Type;
		[String, Required, JsonProperty("webhookId")] public string WebhookId;

		public CreateWebhookSubscriptionInput(
			string[] actions,
			string collectionId,
			string type,
			string webhookId)
		{
			Actions = actions;
			CollectionId = collectionId;
			Type = type;
			WebhookId = webhookId;
		}
	}
}