// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class GetTransactionInput : IInput
	{
		[TransactionHash, Required, JsonProperty("transactionHash")]
		public string TransactionHash;

		public GetTransactionInput(string transactionHash)
		{
			TransactionHash = transactionHash;
		}
	}
}