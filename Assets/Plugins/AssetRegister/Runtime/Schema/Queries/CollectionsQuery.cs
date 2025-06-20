// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class CollectionsInput : IInput
	{
		[ChainAddress, Required, JsonProperty("addresses")]
		public string[] Addresses;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public CollectionsInput(
			string[] addresses,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Addresses = addresses;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class CollectionsResult : IResult
	{
		[JsonProperty("collections")]
		public CollectionConnection Collections;
	}

	internal class CollectionsQuery : IQuery<CollectionConnection, CollectionsInput>
	{
		public CollectionsInput Input { get; }

		public CollectionsQuery(
			string[] addresses,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new CollectionsInput(addresses, before, after, first, last);
		}
	}

}