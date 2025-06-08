// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AssetRegister.Runtime.Schema.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AssetType
	{
		None,
		[EnumMember(Value = "ERC1155")]
		Erc1155,
		[EnumMember(Value = "ERC721")]
		Erc721,
	}
}