// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RegisterOffChainAssetInput : IInput
	{
		[String, Required, JsonProperty("creatorCollectionId")]
		public string CreatorCollectionId;
		[String, Required, JsonProperty("creatorId")]
		public string CreatorId;
		[String, JsonProperty("tokenId")] public string TokenId;

		public RegisterOffChainAssetInput(string creatorCollectionId, string creatorId, string tokenId)
		{
			CreatorCollectionId = creatorCollectionId;
			CreatorId = creatorId;
			TokenId = tokenId;
		}
	}
}