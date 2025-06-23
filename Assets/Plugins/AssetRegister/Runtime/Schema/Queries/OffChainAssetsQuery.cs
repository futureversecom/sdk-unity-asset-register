// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class OffChainAssetsInputWrapper : IInput
	{
		[Required, JsonProperty("input")]
		public OffChainAssetsInput Input;
		[String, JsonProperty("before", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Before;
		[String, JsonProperty("after", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string After;
		[Float, JsonProperty("first", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float First;
		[Float, JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float Last;

		public OffChainAssetsInputWrapper(
			OffChainAssetsInput input,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = input;
			Before = before;
			After = after;
			First = first;
			Last = last;
		}
	}

	[JsonObject]
	public sealed class OffChainAssetsResult : IResult
	{
		[JsonProperty("offChainAssets")]
		public OffChainAssetsConnection OffChainAssets;
	}

	internal class OffChainAssetsQuery : IQuery<OffChainAssetsConnection, OffChainAssetsInputWrapper>
	{
		public string QueryName => "offChainAssets";
		public OffChainAssetsInputWrapper Input { get; }

		public OffChainAssetsQuery(
			OffChainAssetsInput input,
			string before = default,
			string after = default,
			float first = default,
			float last = default
		)
		{
			Input = new OffChainAssetsInputWrapper(input, before, after, first, last);
		}
	}

}