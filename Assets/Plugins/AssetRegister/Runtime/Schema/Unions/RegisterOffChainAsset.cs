// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Unions
{
	[JsonObject]
	public class RegisterOffChainAsset : IUnion
	{
		
	}

	[JsonObject]
	public sealed class RegisterOffChainAssetSuccess : RegisterOffChainAsset
	{
		[JsonProperty("offChainAsset")] public OffChainAsset OffChainAsset;
	}
	
	[JsonObject]
	public sealed class RegisterOffChainAssetFailure : RegisterOffChainAsset
	{
		[JsonProperty("errors")] public AssetRegisterError Errors;
	}
}