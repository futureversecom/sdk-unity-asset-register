// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class SubmitTransactionInput : IInput
	{
		[Boolean, JsonProperty("check")] public bool Check;
		[Signature, Required, JsonProperty("signature")] public string Signature;
		[AssetTransactionMessage, Required, JsonProperty("transaction")] public string Transaction;

		public SubmitTransactionInput(bool check, string signature, string transaction)
		{
			Check = check;
			Signature = signature;
			Transaction = transaction;
		}
	}
}