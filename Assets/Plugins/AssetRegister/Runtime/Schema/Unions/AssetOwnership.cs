// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Unions
{
	[JsonObject]
	public class AssetOwnership : IUnion
	{
		[JsonProperty("id")] public string Id;
	}

	[JsonObject]
	public class SFTAssetOwnership : AssetOwnership
	{
		public SFTBalance balanceOf([ChainAddress, Required] string address)
		{
			return BalanceOf;
		}
		
		[JsonProperty("balanceOf")] public SFTBalance BalanceOf;
		[JsonProperty("balancesOf")] public SFTBalance[] BalancesOf;
	}
	
	[JsonObject]
	public class NFTAssetOwnership : AssetOwnership
	{
		[JsonProperty("owner")] public Account Owner;
	}
}