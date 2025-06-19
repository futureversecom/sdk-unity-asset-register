// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class CreateProfileInput : IInput
	{
		[String, Required, JsonProperty("displayName")] public string DisplayName;
		[ChainAddress, Required, JsonProperty("owner")] public string Owner;

		public CreateProfileInput(string displayName, string owner)
		{
			DisplayName = displayName;
			Owner = owner;
		}
	}
}