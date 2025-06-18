// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Avatar : ISchema
	{
		[JsonProperty("asset")] public Asset Asset;
		[JsonProperty("customImage")] public string CustomImage;
	}
}