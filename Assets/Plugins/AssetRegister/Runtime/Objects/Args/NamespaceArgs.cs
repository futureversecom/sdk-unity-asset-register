// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace AssetRegister.Runtime.Objects.Args
{
	[JsonObject]
	public sealed class NamespaceArgs : IArgs
	{
		[JsonProperty("namespace"), ArgumentVariable(true, Scalar.Url)]
		public string Namespace;

		private NamespaceArgs(string @namespace)
		{
			Namespace = @namespace;
		}

		public static NamespaceArgs Create(string @namespace)
		{
			return new NamespaceArgs(@namespace);
		}
	}
}