// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class GetNonceForChainAddressInput : IInput
	{
		[Required, JsonProperty("input")]
		public NonceInput Input;

		public GetNonceForChainAddressInput(NonceInput input)
		{
			Input = input;
		}
	}

	[JsonObject]
	public sealed class GetNonceForChainAddressResult : IResult
	{
		[JsonProperty("getNonceForChainAddress")]
		public int GetNonceForChainAddress;
	}

	internal class GetNonceForChainAddressQuery : IQuery<NoSchema, GetNonceForChainAddressInput>
	{	
		public GetNonceForChainAddressInput Input { get; }

		public GetNonceForChainAddressQuery(NonceInput input)
		{
			Input = new GetNonceForChainAddressInput(input);
		}
	}

}