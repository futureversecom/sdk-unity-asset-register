// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class AssetsByIdsInput : IInput
	{
		[Required, JsonProperty("assetIds")]
		public Schema.Input.AssetInput[] AssetIds;

		public AssetsByIdsInput(Schema.Input.AssetInput[] assetIds)
		{
			AssetIds = assetIds;
		}
	}

	[JsonObject]
	public sealed class AssetsByIdsResult : IResult
	{
		[JsonProperty("assetsByIds")]
		public Asset[] AssetsByIds;
	}

	internal class AssetsByIdsQuery : IQuery<Asset, AssetsByIdsInput>
	{
		public AssetsByIdsInput Input { get; }

		public AssetsByIdsQuery(Schema.Input.AssetInput[] assetIds)
		{
			Input = new AssetsByIdsInput(assetIds);
		}
	}

}