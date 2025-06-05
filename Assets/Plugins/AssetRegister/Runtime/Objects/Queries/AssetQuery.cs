// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Input;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Queries
{
	public sealed class AssetQuery : IQuery<AssetModel, AssetInput>
	{
		public AssetInput Arguments { get; }
		
		public AssetQuery(string collectionId, string tokenId)
		{
			Arguments = AssetInput.Create(collectionId, tokenId);
		}

		public AssetQuery()
		{
			
		}
	}
}