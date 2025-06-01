// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Reflection;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace Plugins.AssetRegister.Runtime
{
	public static class AssetRegisterQuery
	{
		public static QueryBuilder<Asset, AssetQueryVariables> Asset(string collectionId, string tokenId)
		{
			return new QueryBuilder<Asset, AssetQueryVariables>().SetVariables(
				new AssetQueryVariables()
				{
					CollectionId = collectionId,
					TokenId = tokenId,
				}
			);
		}
	}
}