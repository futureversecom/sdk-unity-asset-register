// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RegisterAssetImageInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[String, JsonProperty("tokenId")] public string TokenId;
		[String, Required, JsonProperty("url")] public string Url;

		public RegisterAssetImageInput(string collectionId, string tokenId, string url)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
			Url = url;
		}
	}
}