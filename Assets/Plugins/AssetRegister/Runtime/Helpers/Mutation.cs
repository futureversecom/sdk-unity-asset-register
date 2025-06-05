// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Input;
using AssetRegister.Runtime.Objects.Models;
using AssetRegister.Runtime.Objects.Mutations;

namespace AssetRegister.Runtime.Helpers
{
	public static class Mutation
	{
		public static IMutationSubBuilder<AssetModel, UpdateAssetProfileInput, IMutationBuilder, IMutationData> UpdateAssetProfile(string assetId, string key, string url)
		{
			return RequestBuilder.RequestBuilder.BeginMutation()
				.Add(new UpdateAssetProfileMutation(assetId, key, url));
		}
	}
}