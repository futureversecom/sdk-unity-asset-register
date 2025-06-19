// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class GenericTokenFilter : IInput
	{
		[String, JsonProperty("amountRange")] public string AmountRange;
		[String, Required, JsonProperty("name")] public string Name;
		[String, Required, JsonProperty("symbol")] public string Symbol;

		public GenericTokenFilter(string amountRange, string name, string symbol)
		{
			AmountRange = amountRange;
			Name = name;
			Symbol = symbol;
		}
	}
}