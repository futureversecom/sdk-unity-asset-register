// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class ProfileExperience : NodeBase
	{
		[JsonProperty("experience")] public string Experience;
		[JsonProperty("owner")] public string Owner;
		[JsonProperty("profileId")] public string ProfileId;
	}
}