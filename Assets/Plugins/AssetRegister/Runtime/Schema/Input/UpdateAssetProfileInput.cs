// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateAssetProfileInput : IInput
	{
		[String, Required, JsonProperty("assetId")]
		public string AssetId;
		[String, Required, JsonProperty("key")]
		public string Key;
		[String, Required, JsonProperty("url")]
		public string Url;

		public UpdateAssetProfileInput(string assetId, string key, string url)
		{
			AssetId = assetId;
			Key = key;
			Url = url;
		} 
	}
}