// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateAccountAdditionalPropertyInput : IInput
	{
		[String, Required, JsonProperty("key")]
		public string Key;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;
		[PropertyValue, Required, JsonProperty("value")]
		public string Value;

		public UpdateAccountAdditionalPropertyInput(string key, string owner, string value)
		{
			Key = key;
			Owner = owner;
			Value = value;
		}
	}
}