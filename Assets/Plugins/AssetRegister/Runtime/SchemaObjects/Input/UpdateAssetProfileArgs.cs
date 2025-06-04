// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class UpdateAssetProfileArgs : IArguments
	{
		[JsonProperty("updateAssetProfile_input"), ArgumentVariable(true, "UpdateAssetProfileInput")]
		public InnerArgs Input;

		private UpdateAssetProfileArgs(string assetId, string key, string url)
		{
			Input = new InnerArgs()
			{
				AssetId = assetId,
				Key = key,
				Url = url,
			};
		}

		public static UpdateAssetProfileArgs Create(string assetId, string key, string url)
		{
			return new UpdateAssetProfileArgs(assetId, key, url);
		}
	}

	[JsonObject]
	public class InnerArgs
	{
		[JsonProperty("assetId")]
		public string AssetId;
		[JsonProperty("key")]
		public string Key;
		[JsonProperty("url")]
		public string Url;
	}
}