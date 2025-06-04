// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace AssetRegister.Runtime.Objects.Args
{
	[JsonObject]
	public sealed class AssetArgs : IArgs
	{
		[JsonProperty("tokenId"), ArgumentVariable(true, Scalar.String)]
		public string TokenId;
		[JsonProperty("collectionId"), ArgumentVariable(true, Scalar.CollectionId)]
		public string CollectionId;

		private AssetArgs(string collectionId, string tokenId)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}

		public static AssetArgs Create(string collectionId, string tokenId)
		{
			return new AssetArgs(collectionId, tokenId);
		}
	}
}