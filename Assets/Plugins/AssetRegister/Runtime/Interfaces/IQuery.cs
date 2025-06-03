// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace Plugins.AssetRegister.Runtime.Interfaces
{
	public interface IQuery<out TFields, out TArguments>
		where TFields : class, IModel
		where TArguments : class, IArguments
	{
		TArguments Arguments { get; }
	}
	
	[JsonObject]
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

	[JsonObject]
	public class NamespaceQuery : IQuery<NamespaceModel, NamespaceArgs>
	{
		public NamespaceArgs Arguments { get; }

		public NamespaceQuery(string @namespace)
		{
			Arguments = NamespaceArgs.Create(@namespace);
		}

		public NamespaceQuery()
		{
			
		}
	}
}