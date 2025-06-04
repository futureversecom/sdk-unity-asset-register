// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq.Expressions;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class AssetArgs : IArguments
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