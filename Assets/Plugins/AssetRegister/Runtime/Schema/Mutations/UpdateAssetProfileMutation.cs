// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;

namespace AssetRegister.Runtime.Schema.Mutations
{
	public sealed class UpdateAssetProfileMutation : IMutation<Asset, UpdateAssetProfileInput>
	{
		public string FunctionName => "updateAssetProfile";
		public UpdateAssetProfileInput Arguments { get; }

		public UpdateAssetProfileMutation(string assetId, string key, string url)
		{ 
			Arguments = UpdateAssetProfileInput.Create(assetId, key, url);
		}

		public UpdateAssetProfileMutation()
		{
			
		}
	}
}