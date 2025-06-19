// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RemoveProfileAdditionalPropertyInput : IInput
	{
		[String, Required, JsonProperty("key")]
		public string Key;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;

		public RemoveProfileAdditionalPropertyInput(string key, string owner, string profileId)
		{
			Key = key;
			Owner = owner;
			ProfileId = profileId;
		}
	}
}