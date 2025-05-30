// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Models;

namespace Plugins.AssetRegister.Runtime.Input
{
	[JsonObject]
	public class AssetInput : IInput<Asset>
	{
		[JsonProperty("tokenId")] public string TokenId;
		[JsonProperty("collectionId")] /*CollectionId*/ public string CollectionId;
	}
}