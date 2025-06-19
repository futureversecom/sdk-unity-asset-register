// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class AssetFilter : IInput
	{
		[JsonProperty("eqFilters", DefaultValueHandling = DefaultValueHandling.Ignore)] public EqFilter[] EqFilters;
		[String, JsonProperty("hasFilters", DefaultValueHandling = DefaultValueHandling.Ignore)] public string[] HasFilters;
		[String, JsonProperty("search", DefaultValueHandling = DefaultValueHandling.Ignore)] public string Search;

		public AssetFilter(EqFilter[] eqFilters = default, string[] hasFilters = default, string search = default)
		{
			EqFilters = eqFilters;
			HasFilters = hasFilters;
			Search = search;
		}
	}
}