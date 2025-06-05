// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Input;
using AssetRegister.Runtime.Objects.Models;
using AssetRegister.Runtime.Objects.Queries;

namespace AssetRegister.Runtime.Helpers
{
	public static class Query
	{
		public static IQuerySubBuilder<AssetModel, AssetInput, IQueryBuilder, IQueryData> Asset(string collectionId, string tokenId)
		{
			return RequestBuilder.RequestBuilder.BeginQuery()
				.Add(new AssetQuery(collectionId, tokenId));
		}
	}
}