// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Unions
{
	[JsonObject]
	public class AssetOperationResponse : IUnion
	{
		
	}

	[JsonObject]
	public sealed class AssetOperationResultSuccess : AssetOperationResponse
	{
		[JsonProperty("transactionHash")] public string TransactionHash;
	}
	
	[JsonObject]
	public sealed class AssetOperationResultFailure : AssetOperationResponse
	{
		[JsonProperty("errors")] public AssetOperationResultError[] Errors;
	}
}