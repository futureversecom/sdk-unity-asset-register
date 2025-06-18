// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Schema.Interfaces;

namespace AssetRegister.Runtime.Schema.Objects
{
	[JsonObject]
	public class ErrorBase : IError
	{
		[JsonProperty("message")] public string Message { get; }
	}
	
	[JsonObject]
	public sealed class AssetRegisterError : ErrorBase
	{
		[JsonProperty("extensions")] public AssetRegisterErrorExtensions Extensions;
	}
	
	[JsonObject]
	public sealed class TransactionError : ErrorBase
	{
		[JsonProperty("code")] public float Code;
	}
	
	[JsonObject]
	public sealed class AssetOperationResultError : ErrorBase { }
	
	[JsonObject]
	public sealed class CreateSchemaError : ErrorBase { }
}