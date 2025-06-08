// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;

namespace Plugins.AssetRegisterV2.Runtime.Schemas
{
	public interface IAsset : IModel
	{
		string Id { get; }
		AssetOwnership Ownership { get; }
		Account Account { get; }
	}

	public class Asset : IAsset
	{
		[JsonProperty("id")] public string Id { get; }
		[JsonProperty("ownership")] public AssetOwnership Ownership { get; }
		[JsonProperty("assetType")] public string AssetType { get; }
		[JsonProperty("account")] public Account Account { get; }
	}
	
	[JsonObject]
	public class Account
	{
		[JsonProperty("handle")] public string Handle;
		[JsonProperty("id")] public string Id;
	}
}