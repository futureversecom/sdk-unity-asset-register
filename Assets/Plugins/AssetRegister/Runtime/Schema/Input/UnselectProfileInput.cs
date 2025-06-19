// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UnselectProfileInput : IInput
	{
		[String, Required, JsonProperty("experience")]
		public string Experience;
		[ChainAddress, Required, JsonProperty("owner")]
		public string Owner;

		public UnselectProfileInput(string experience, string owner)
		{
			Experience = experience;
			Owner = owner;
		}
	}
}