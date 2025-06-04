// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects.Mutations
{
	public class UpdateAssetProfileMutation : IMutation<AssetModel, UpdateAssetProfileArgs>
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