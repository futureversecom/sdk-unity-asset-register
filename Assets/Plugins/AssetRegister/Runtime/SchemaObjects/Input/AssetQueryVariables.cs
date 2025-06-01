// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq.Expressions;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class AssetQueryVariables : IQueryVariables
	{
		[JsonProperty("tokenId"), QueryInputVariable(true, ScalarType.String)]
		public string TokenId;
		[JsonProperty("collectionId"), QueryInputVariable(true, ScalarType.CollectionId)]
		public string CollectionId;
	}
}