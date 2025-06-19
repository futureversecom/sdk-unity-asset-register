// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class OffChainAssetsInput : IInput
	{
		[String, Required, JsonProperty("creatorId")]
		public string CreatorId;

		public OffChainAssetsInput(string creatorId)
		{
			CreatorId = creatorId;
		}
	}
}