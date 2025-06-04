// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Args;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Queries
{
	public sealed class AssetQuery : IQuery<AssetModel, AssetArgs>
	{
		public AssetArgs Arguments { get; }
		
		public AssetQuery(string collectionId, string tokenId)
		{
			Arguments = AssetArgs.Create(collectionId, tokenId);
		}

		public AssetQuery()
		{
			
		}
	}
}