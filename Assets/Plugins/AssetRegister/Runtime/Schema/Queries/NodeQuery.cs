// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Schema.Interfaces;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Schema.Queries
{
	[JsonObject]
	public sealed class NodeInput : IInput
	{
		[String, Required, JsonProperty("id")]
		public string Id;

		public NodeInput(string id)
		{
			Id = id;
		}
	}

	[JsonObject]
	public sealed class NodeResult : IResult
	{
		[JsonProperty("node"), JsonConverter(typeof(InterfaceConverter))]
		public INode Node;
	}

	internal class NodeQuery : IQuery<INode, NodeInput>
	{
		public NodeInput Input { get; }

		public NodeQuery(string id)
		{
			Input = new NodeInput(id);
		}
	}

}