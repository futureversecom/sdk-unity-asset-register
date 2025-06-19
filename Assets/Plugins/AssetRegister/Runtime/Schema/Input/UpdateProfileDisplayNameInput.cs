// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateProfileDisplayNameInput : IInput
	{
		[String, Required, JsonProperty("displayName")]
		public string DisplayName;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;

		public UpdateProfileDisplayNameInput(string displayName, string owner, string profileId)
		{
			DisplayName = displayName;
			Owner = owner;
			ProfileId = profileId;
		}
	}
}