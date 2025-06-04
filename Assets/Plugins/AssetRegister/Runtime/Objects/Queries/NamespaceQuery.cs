// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Args;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Queries
{
	public sealed class NamespaceQuery : IQuery<NamespaceModel, NamespaceArgs>
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