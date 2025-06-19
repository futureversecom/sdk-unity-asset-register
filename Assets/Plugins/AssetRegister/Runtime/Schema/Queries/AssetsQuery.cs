// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public class AssetsQueryInput : IInput
	{
		[Boolean, JsonProperty("removeDuplicates", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool RemoveDuplicates;
		
	}

	[JsonObject]
	public class AssetsQueryResult : IResult
	{
		[JsonProperty("assets")] public AssetConnection[] Assets;
	}
	
	internal class AssetsQuery : IQuery<AssetConnection, AssetsQueryInput>
	{
		public AssetsQueryInput Input { get; }

		public AssetsQuery()
		{
			
		}
	}
}