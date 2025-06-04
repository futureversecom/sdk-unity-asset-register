// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Args;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Mutations
{
	public sealed class UpdateAssetProfileMutation : IMutation<AssetModel, UpdateAssetProfileArgs>
	{
		public string FunctionName => "updateAssetProfile";
		public UpdateAssetProfileArgs Arguments { get; }

		public UpdateAssetProfileMutation(string assetId, string key, string url)
		{ 
			Arguments = UpdateAssetProfileArgs.Create(assetId, key, url);
		}

		public UpdateAssetProfileMutation()
		{
			
		}
	}
}