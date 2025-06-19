// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class SelectProfileInput : IInput
	{
		[String, Required, JsonProperty("experience")]
		public string Experience;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[String, Required, JsonProperty("profileId")]
		public string ProfileId;

		public SelectProfileInput(string experience, string owner, string profileId)
		{
			Experience = experience;
			Owner = owner;
			ProfileId = profileId;
		}
	}
}