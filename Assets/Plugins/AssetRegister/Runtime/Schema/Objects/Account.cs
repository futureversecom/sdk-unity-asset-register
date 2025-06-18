// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Account : NodeBase
	{
		[JsonProperty("additionalProperties")] public JToken AdditionalProperties;
		[JsonProperty("address")] public string Address;
		[JsonProperty("eoa")] public string EOA;
		[JsonProperty("futurepass")] public string Futurepass;
		[JsonProperty("handle")] public string Handle;
		[JsonProperty("lastUsedProfileByExperience")] public Profile LastUsedProfileByExperience;
		[JsonProperty("profiles")] public Profile[] Profiles;
		[JsonProperty("rns")] public RNS RNS;
	}
}