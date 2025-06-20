// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class TransactionInput : IInput
	{
		[TransactionHash, Required, JsonProperty("transactionHash")]
		public string TransactionHash;

		public TransactionInput(string transactionHash)
		{
			TransactionHash = transactionHash;
		}
	}

	[JsonObject]
	public sealed class TransactionResult : IResult
	{
		[JsonProperty("transaction")]
		public Transaction Transaction;
	}

	internal class TransactionQuery : IQuery<Transaction, TransactionInput>
	{
		public TransactionInput Input { get; }

		public TransactionQuery(string transactionHash)
		{
			Input = new TransactionInput(transactionHash);
		}
	}

}