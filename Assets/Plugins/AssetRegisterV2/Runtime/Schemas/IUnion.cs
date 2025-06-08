// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegisterV2.Runtime
{
	public interface IUnion
	{
		
	}
	
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
	}
	
	[JsonObject]
	public class SFTBalance
	{
		[JsonProperty("balance")] public float Balance;
		[JsonProperty("id")] public string Id;
	}
}