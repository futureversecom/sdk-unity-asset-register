// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Profile : NodeBase
	{
		[JsonProperty("additionalProperties")] public JToken AdditionalProperties;
		[JsonProperty("avatar")] public Avatar Avatar;
		[JsonProperty("displayName")] public string DisplayName;
		[JsonProperty("owner")] public string Owner;
		[JsonProperty("profileId")] public string ProfileId;
		[JsonProperty("profileProperties")] public ProfileProperties ProfileProperties;
	}
}