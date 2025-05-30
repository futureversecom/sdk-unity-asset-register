// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Reflection;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Models;

namespace Plugins.AssetRegister.Runtime
{
	public static class Queries
	{
		[AssetRegisterQuery("asset")]
		public static AssetRegisterQueryBuilder<Asset> AssetQuery(string collectionId, string tokenId)
		{
			return AssetRegisterQueryBuilder<Asset>.FromMethod(MethodBase.GetCurrentMethod(), collectionId, tokenId);
		}
	}
}