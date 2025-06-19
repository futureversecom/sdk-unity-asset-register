// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Enums;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class Sort : IInput
	{
		[String, Required, JsonProperty("name")]
		public string Name;
		[Required, JsonProperty("order")] public SortOrder Order;

		public Sort(string name, SortOrder order)
		{
			Name = name;
			Order = order;
		}
	}
}