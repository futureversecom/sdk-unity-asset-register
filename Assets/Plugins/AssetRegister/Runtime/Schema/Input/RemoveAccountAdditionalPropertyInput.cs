// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RemoveAccountAdditionalPropertyInput : IInput
	{
		[String, Required, JsonProperty("key")]
		public string Key;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;

		public RemoveAccountAdditionalPropertyInput(string key, string owner)
		{
			Key = key;
			Owner = owner;
		}
	}
}