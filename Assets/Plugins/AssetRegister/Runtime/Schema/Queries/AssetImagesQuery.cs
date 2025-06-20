// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class AssetImagesInput : IInput
	{
		[CollectionId, Required, JsonProperty("collectionId")]
		public string CollectionId;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[String, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[String, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public AssetImagesInput(string collectionId, string before = default, string after = default, float first = default, float last = default)
		{
			CollectionId = collectionId;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}
	
	[JsonObject]
	public sealed class AssetImagesResult : IResult
	{
		[JsonProperty("assetImages")] public AssetImagesConnection[] AssetImages;
	}
	
	internal class AssetImagesQuery : IQuery<AssetImagesConnection, AssetImagesInput>
	{
		public AssetImagesInput Input { get; }
		
		public AssetImagesQuery(string collectionId, string before = default, string after = default, float first = default, float last = default)
		{
			Input = new AssetImagesInput(collectionId, before, after, first, last);
		}
	}
}