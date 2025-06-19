// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class AssetInput : IInput
	{
		[String, Required, JsonProperty("tokenId")]
		public string TokenId;
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;

		public AssetInput(string collectionId, string tokenId)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}
	}
	
	[JsonObject]
	public sealed class AssetResult : IResult
	{
		[JsonProperty("asset")]
		public Asset Asset;
	}
	
	internal class AssetQuery : IQuery<Asset, AssetInput>
	{
		public AssetInput Input { get; }
		
		internal AssetQuery(string collectionId, string tokenId)
		{
			Input = new AssetInput(collectionId, tokenId);
		}
	}
}