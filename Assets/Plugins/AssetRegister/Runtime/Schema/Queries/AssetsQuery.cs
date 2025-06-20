// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
public sealed class AssetsInput : IInput
{
    [Boolean, JsonProperty("removeDuplicates", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool RemoveDuplicates;
    [JsonProperty("sort", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Sort[] Sort;
    [JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public AssetFilter Filter;
    [SchemaIdentifier, JsonProperty("schemaId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string SchemaId;
    [CollectionId, JsonProperty("collectionIds", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[] CollectionIds;
    [ChainAddress, JsonProperty("addresses", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[] Addresses;
    [String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Before;
    [String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string After;
    [Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public float First;
    [Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public float Last;
    [String, JsonProperty("chainId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string ChainId;
    [String, JsonProperty("chainType", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string ChainType;

    public AssetsInput(
        bool removeDuplicates = default,
        Sort[] sort = default,
        AssetFilter filter = default,
        string schemaId = default,
        string[] collectionIds = default,
        string[] addresses = default,
        string before = default,
        string after = default,
        float first = default,
        float last = default,
        string chainId = default,
        string chainType = default
    )
    {
        RemoveDuplicates = removeDuplicates;
        Sort = sort;
        Filter = filter;
        SchemaId = schemaId;
        CollectionIds = collectionIds;
        Addresses = addresses;
        Before = before;
        After = after;
        First = first;
        Last = last;
        ChainId = chainId;
        ChainType = chainType;
    }
}

[JsonObject]
public sealed class AssetsResult : IResult
{
    [JsonProperty("assets")]
    public AssetConnection Assets;
}

internal class AssetsQuery : IQuery<AssetConnection, AssetsInput>
{
    public AssetsInput Input { get; }

    public AssetsQuery(
        bool removeDuplicates = default,
        Sort[] sort = default,
        AssetFilter filter = default,
        string schemaId = default,
        string[] collectionIds = default,
        string[] addresses = default,
        string before = default,
        string after = default,
        float first = default,
        float last = default,
        string chainId = default,
        string chainType = default
    )
    {
        Input = new AssetsInput(
            removeDuplicates,
            sort,
            filter,
            schemaId,
            collectionIds,
            addresses,
            before,
            after,
            first,
            last,
            chainId,
            chainType
        );
    }
}

}