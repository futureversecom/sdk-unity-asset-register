// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class SetProfileDescriptionInput : IInput
	{
		[String, Required, JsonProperty("description")]
		public string Description;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;

		public SetProfileDescriptionInput(string description, string owner, string profileId)
		{
			Description = description;
			Owner = owner;
			ProfileId = profileId;
		}
	}
}