// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class AssetMutationInput : IInput
	{
		[Signature, Required, JsonProperty("signature")] public string Signature;
		[AssetTransactionMessage, Required, JsonProperty("transaction")] public string Transaction;

		public AssetMutationInput(string signature, string transaction)
		{
			Signature = signature;
			Transaction = transaction;
		}
	}
}