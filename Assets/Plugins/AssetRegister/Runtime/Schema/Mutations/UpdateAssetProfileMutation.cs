// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Mutations
{
	[JsonObject("updateAssetProfile")]
	public class UpdateAssetProfile : IModel
	{
		[JsonProperty("asset")] public Asset Asset;
	}
	
	public sealed class UpdateAssetProfileMutation : IMutation<UpdateAssetProfile, UpdateAssetProfileInput>
	{
		public UpdateAssetProfileInput Input { get; }

		public UpdateAssetProfileMutation(string assetId, string key, string url)
		{ 
			Input = UpdateAssetProfileInput.Create(assetId, key, url);
		}
	}
}