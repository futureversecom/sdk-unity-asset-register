// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.SchemaObjects
{
	[JsonObject]
	public class PageInfo
	{
		[JsonProperty("endCursor")] public string EndCursor;
		[JsonProperty("hasNextPage")] public bool HasNextPage;
		[JsonProperty("hasPreviousPage")] public bool HasPreviousPage;
		[JsonProperty("startCursor")] public string StartCursor;
		[JsonProperty("nextPage")] public string NextPage;
	}
}