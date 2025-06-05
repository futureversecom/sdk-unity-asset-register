// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Input;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Mutations
{
	public sealed class UpdateAssetProfileMutation : IMutation<AssetModel, UpdateAssetProfileInput>
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