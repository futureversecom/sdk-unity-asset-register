// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public sealed class CollectionDelegate : NodeBase
	{
		[JsonProperty("collection")] public Collection Collection;
		[JsonProperty("delegateAddress")] public string DelegateAddress;
	}
}