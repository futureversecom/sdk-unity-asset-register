// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects.Queries
{
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