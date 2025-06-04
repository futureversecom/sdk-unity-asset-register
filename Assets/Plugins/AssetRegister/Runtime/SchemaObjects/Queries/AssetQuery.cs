// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects.Queries
{
	public class AssetQuery : IQuery<AssetModel, AssetArgs>
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