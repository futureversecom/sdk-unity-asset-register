// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class EqFilter : IInput
	{
		[String, Required, JsonProperty("name")]
		public string Name;
		[String, Required, JsonProperty("value")]
		public string Value;

		public EqFilter(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}