// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class GenericTokenBalancesInput : IInput
	{
		[JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public GenericTokenFilter Filter;
		[ChainAddress, Required, JsonProperty("addresses")]
		public string[] Addresses;
		[GenericTokenId, JsonProperty("genericTokenIds", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] GenericTokenIds;

		public GenericTokenBalancesInput(
			string[] addresses,
			GenericTokenFilter filter = default,
			string[] genericTokenIds = default
		)
		{
			Addresses = addresses;
			Filter = filter;
			GenericTokenIds = genericTokenIds;
		}
	}

	[JsonObject]
	public sealed class GenericTokenBalancesResult : IResult
	{
		[JsonProperty("genericTokenBalances")]
		public GenericTokenBalance[] GenericTokenBalances;
	}

	internal class GenericTokenBalancesQuery : IQuery<GenericTokenBalance, GenericTokenBalancesInput>
	{
		public GenericTokenBalancesInput Input { get; }

		public GenericTokenBalancesQuery(
			string[] addresses,
			GenericTokenFilter filter = default,
			string[] genericTokenIds = default
		)
		{
			Input = new GenericTokenBalancesInput(addresses, filter, genericTokenIds);
		}
	}

}