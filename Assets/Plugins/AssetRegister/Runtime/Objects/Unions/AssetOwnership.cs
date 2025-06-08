// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Objects.Schemas;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Objects.Unions.AssetOwnership
{
	[JsonObject]
	public class AssetOwnership : IUnion
	{
		[JsonProperty("id")] public string Id;
	}

	[JsonObject]
	public class SFTAssetOwnership : AssetOwnership
	{
		public SFTBalance _BalanceOf(string address)
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