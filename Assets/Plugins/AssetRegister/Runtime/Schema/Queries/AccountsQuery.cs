// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class AccountsInput : IInput
	{
		[ChainAddress, Required, JsonProperty("addresses")]
		public string[] Addresses;

		public AccountsInput(string[] addresses)
		{
			Addresses = addresses;
		}
	}
	
	[JsonObject]
	public sealed class AccountsResult : IResult
	{
		[JsonProperty("accounts")] public Account[] Accounts;
	}
	
	internal class AccountsQuery : IQuery<Account, AccountsInput>
	{
		public string QueryName => "accounts";
		public AccountsInput Input { get; }
		
		public AccountsQuery(string[] addresses)
		{
			Input = new AccountsInput(addresses);
		}
	}
}