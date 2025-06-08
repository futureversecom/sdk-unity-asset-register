// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class NamespaceInput : IInput
	{
		[Url, Required, JsonProperty("namespace")]
		public string Namespace;

		private NamespaceInput(string @namespace)
		{
			Namespace = @namespace;
		}

		public static NamespaceInput Create(string @namespace)
		{
			return new NamespaceInput(@namespace);
		}
	}
}