// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Base type for all schema objects
	/// </summary>
	public interface ISchema { }
	/// <summary>
	/// Represents a GraphQL union type
	/// </summary>
	public interface IUnion : ISchema { }
	public interface IInterface : ISchema { }
	public interface IResult : ISchema { }
	/// <summary>
	/// Represents input to a GraphQL query or mutation
	/// </summary>
	public interface IInput { }
}