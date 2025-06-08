// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;

namespace AssetRegister.Runtime.Schema.Queries
{
	public sealed class NamespaceQuery : IQuery<Namespace, NamespaceInput>
	{
		public NamespaceInput Input { get; }

		public NamespaceQuery(string @namespace)
		{
			Input = NamespaceInput.Create(@namespace);
		}

		public NamespaceQuery()
		{
			
		}
	}
}