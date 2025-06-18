// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Transactions;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class Transaction : ISchema
	{
		[JsonProperty("createdAt")] public float CreatedAt;
		[JsonProperty("error")] public TransactionError Error;
		[JsonProperty("events")] public TransactionEvent[] Events;
		[JsonProperty("id")] public string Id;
		[JsonProperty("status")] public string Status;
		[JsonProperty("transactionHash")] public string TransactionHash;
	}
	
	[JsonObject]
	public sealed class TransactionEdge : EdgeBase<Transaction> { }
	[JsonObject]
	public sealed class TransactionConnection : ConnectionBase<TransactionEdge> { }
}