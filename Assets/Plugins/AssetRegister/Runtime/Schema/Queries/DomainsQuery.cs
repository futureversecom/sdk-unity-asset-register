// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class DomainsInput : IInput
	{
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public DomainsInput(
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class DomainsResult : IResult
	{
		[JsonProperty("domains")]
		public DomainsConnection Domains;
	}

	internal class DomainsQuery : IQuery<DomainsConnection, DomainsInput>
	{
		public string QueryName => "domains";
		public DomainsInput Input { get; }

		public DomainsQuery(
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new DomainsInput(before, after, first, last);
		}
	}

}