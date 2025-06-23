// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class OffChainAssetInput : IInput
	{
		[String, JsonProperty("tokenId", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TokenId;
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;

		public OffChainAssetInput(string collectionId, string tokenId = default)
		{
			CollectionId = collectionId;
			TokenId = tokenId;
		}
	}

	[JsonObject]
	public sealed class OffChainAssetResult : IResult
	{
		[JsonProperty("offChainAsset")]
		public OffChainAsset OffChainAsset;
	}

	internal class OffChainAssetQuery : IQuery<OffChainAsset, OffChainAssetInput>
	{
		public string QueryName => "offChainAsset";
		public OffChainAssetInput Input { get; }

		public OffChainAssetQuery(string collectionId, string tokenId = default)
		{
			Input = new OffChainAssetInput(collectionId, tokenId);
		}
	}

}