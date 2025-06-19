// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateProfileAdditionalPropertyInput : IInput
	{
		[String, Required, JsonProperty("key")]
		public string Key;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;
		[PropertyValue, Required, JsonProperty("value")]
		public string Value;

		public UpdateProfileAdditionalPropertyInput(
			string key,
			string owner,
			string profileId,
			string value)
		{
			Key = key;
			Owner = owner;
			ProfileId = profileId;
			Value = value;
		}
	}
}