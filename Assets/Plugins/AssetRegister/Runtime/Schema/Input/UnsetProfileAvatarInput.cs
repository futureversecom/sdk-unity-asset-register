// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UnsetProfileAvatarInput : IInput
	{
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;

		public UnsetProfileAvatarInput(string owner, string profileId)
		{
			Owner = owner;
			ProfileId = profileId;
		}
	}
}