// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class NonceInput : IInput
	{
		[ChainAddress, Required, JsonProperty("chainAddress")]
		public string ChainAddress;

		public NonceInput(string chainAddress)
		{
			ChainAddress = chainAddress;
		}
	}
}