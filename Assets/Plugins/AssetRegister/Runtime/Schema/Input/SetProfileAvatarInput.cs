// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class SetProfileAvatarInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;
		[String, Required, JsonProperty("tokenId")]
		public string TokenId;

		public SetProfileAvatarInput(
			string collectionId,
			string owner,
			string profileId,
			string tokenId)
		{
			CollectionId = collectionId;
			Owner = owner;
			ProfileId = profileId;
			TokenId = tokenId;
		}
	}
}