// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Objects.Schemas;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Objects.Models
{
	[JsonObject, GraphQLModel("namespace")]
	public sealed class NamespaceModel : IModel
	{
		[JsonProperty("id")] public string Id;
		[JsonProperty("schemas")] public SchemasConnection Schemas;
		[JsonProperty("url")] public string Url;
	}
}