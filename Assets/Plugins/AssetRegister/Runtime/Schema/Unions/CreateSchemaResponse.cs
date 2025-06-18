// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Schema.Unions
{
	[JsonObject]
	public class CreateSchemaResponse : IUnion
	{
		
	}

	[JsonObject]
	public sealed class CreateSchemaSuccess : CreateSchemaResponse
	{
		[JsonProperty("schema")] public Objects.Schema Schema;
	}

	[JsonObject]
	public sealed class CreateSchemaFailure : CreateSchemaResponse
	{
		[JsonProperty("errors")] public CreateSchemaError[] Errors;
	}
}