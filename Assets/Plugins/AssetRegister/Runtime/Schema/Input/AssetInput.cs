// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class AssetInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[String, Required, JsonProperty("tokenId")] public string TokenId;

		public AssetInput(string collectionId, string tokenId)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}
	}
}