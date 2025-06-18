// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AssetRegister.Runtime.Schema.Enums
{
	public class SortOrder
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public enum AssetType
		{
			[EnumMember(Value = "ASC")]
			Ascending,
			[EnumMember(Value = "DESC")]
			Descending,
		}
	}
}