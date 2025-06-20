// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class TransactionsInput : IInput
	{
		[ChainAddress, Required, JsonProperty("address")]
		public string Address;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public TransactionsInput(
			string address,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Address = address;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class TransactionsResult : IResult
	{
		[JsonProperty("transactions")]
		public TransactionsConnection Transactions;
	}

	internal class TransactionsQuery : IQuery<TransactionsConnection, TransactionsInput>
	{
		public TransactionsInput Input { get; }

		public TransactionsQuery(
			string address,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new TransactionsInput(address, before, after, first, last);
		}
	}

}