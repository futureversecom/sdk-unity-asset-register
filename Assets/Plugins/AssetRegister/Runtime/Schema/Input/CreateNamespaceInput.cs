// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class CreateNamespaceInput : IInput
	{
		[String, Required, JsonProperty("domain")] public string Domain;
		[String, Required, JsonProperty("suffix")] public string Suffix;

		public CreateNamespaceInput(string domain, string suffix)
		{
			Domain = domain;
			Suffix = suffix;
		}
	}
}