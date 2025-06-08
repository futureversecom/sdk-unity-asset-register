// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class UpdateAssetProfileInput : IInput
	{
		[CustomType("UpdateAssetProfileInput"), Required, JsonProperty("updateAssetProfile_input")]
		public UpdateAssetProfileInnerInput Input;

		private UpdateAssetProfileInput(string assetId, string key, string url)
		{
			Input = new UpdateAssetProfileInnerInput()
			{
				AssetId = assetId,
				Key = key,
				Url = url,
			};
		}

		public static UpdateAssetProfileInput Create(string assetId, string key, string url)
		{
			return new UpdateAssetProfileInput(assetId, key, url);
		}
	}

	[JsonObject]
	public class UpdateAssetProfileInnerInput
	{
		[JsonProperty("assetId")]
		public string AssetId;
		[JsonProperty("key")]
		public string Key;
		[JsonProperty("url")]
		public string Url;
	}
}