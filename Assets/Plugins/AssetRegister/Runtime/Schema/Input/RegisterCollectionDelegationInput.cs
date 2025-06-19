// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Input
{
	[JsonObject]
	public sealed class RegisterCollectionDelegationInput : IInput
	{
		[ChainAddress, Required, JsonProperty("address")]
		public string Address;
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;

		public RegisterCollectionDelegationInput(string address, string collectionId)
		{
			Address = address;
			CollectionId = collectionId;
		} 
	}
}