// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Unions
{
	[JsonObject]
	public class AssetLink : NodeBase, IUnion { }

	[JsonObject]
	public sealed class SFTAssetLink : AssetLink
	{
		[JsonProperty("parentLinks")] public Asset[] ParentLinks;
	}

	[JsonObject]
	public sealed class NFTAssetLink : AssetLink
	{
		[JsonProperty("parentLink")] public Asset ParentLink;
		[JsonProperty("childLinks")] public Link[] ChildLinks;
	}
}