// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class NamespaceArgs : IArguments
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