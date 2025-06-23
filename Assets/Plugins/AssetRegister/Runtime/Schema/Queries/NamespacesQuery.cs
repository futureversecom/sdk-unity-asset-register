// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class NamespacesInput : IInput
	{
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public NamespacesInput(
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
	public sealed class NamespacesResult : IResult
	{
		[JsonProperty("namespaces")]
		public NamespacesConnection Namespaces;
	}

	internal class NamespacesQuery : IQuery<NamespacesConnection, NamespacesInput>
	{
		public string QueryName => "namespaces";
		public NamespacesInput Input { get; }

		public NamespacesQuery(
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new NamespacesInput(before, after, first, last);
		}
	}

}