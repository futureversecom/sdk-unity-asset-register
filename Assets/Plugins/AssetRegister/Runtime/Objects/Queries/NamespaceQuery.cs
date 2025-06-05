// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Input;
using AssetRegister.Runtime.Objects.Models;

namespace AssetRegister.Runtime.Objects.Queries
{
	public sealed class NamespaceQuery : IQuery<NamespaceModel, NamespaceInput>
	{
		public NamespaceInput Arguments { get; }

		public NamespaceQuery(string @namespace)
		{
			Arguments = NamespaceInput.Create(@namespace);
		}

		public NamespaceQuery()
		{
			
		}
	}
}