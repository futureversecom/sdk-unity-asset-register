// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class OwnersInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionIds")]
		public string[] CollectionIds;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public OwnersInput(
			string[] collectionIds,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			CollectionIds = collectionIds;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class OwnersResult : IResult
	{
		[JsonProperty("owners")]
		public AssetOwnersConnection Owners;
	}

	internal class OwnersQuery : IQuery<AssetOwnersConnection, OwnersInput>
	{
		public string QueryName => "owners";
		public OwnersInput Input { get; }

		public OwnersQuery(
			string[] collectionIds,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new OwnersInput(collectionIds, before, after, first, last);
		}
	}
}