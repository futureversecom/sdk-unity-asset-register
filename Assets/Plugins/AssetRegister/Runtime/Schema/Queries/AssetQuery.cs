// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;

namespace AssetRegister.Runtime.Schema.Queries
{
	public sealed class AssetQuery : IQuery<Asset, AssetInput>
	{
		public AssetInput Input { get; }
		
		public AssetQuery(string collectionId, string tokenId)
		{
			Input = AssetInput.Create(collectionId, tokenId);
		}
	}
}