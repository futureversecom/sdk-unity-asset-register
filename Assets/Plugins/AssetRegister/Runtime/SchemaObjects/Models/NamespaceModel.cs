// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject, GraphQLModel("namespace")]
	public class NamespaceModel : IModel
	{
		[JsonProperty("id")] public string Id;
		[JsonProperty("schemas")] public SchemasConnection Schemas;
		[JsonProperty("url")] public string Url;
	}
}