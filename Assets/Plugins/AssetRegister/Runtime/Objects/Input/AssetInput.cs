// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Objects.Input
{
	[JsonObject]
	public sealed class AssetInput : IInput
	{
		[JsonProperty("tokenId"), ArgumentVariable(true, Scalar.String)]
		public string TokenId;
		[JsonProperty("collectionId"), ArgumentVariable(true, Scalar.CollectionId)]
		public string CollectionId;

		private AssetInput(string collectionId, string tokenId)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}

		public static AssetInput Create(string collectionId, string tokenId)
		{
			return new AssetInput(collectionId, tokenId);
		}
	}
}