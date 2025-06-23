// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Represents a single GraphQL query. Can be added to a QueryBuilder
	/// </summary>
	/// <typeparam name="TSchema"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	public interface IQuery<out TSchema, out TInput>
		where TSchema : ISchema
		where TInput : class, IInput
	{
		string QueryName { get; }
		TInput Input { get; }
	}
	
	public class NoSchema : ISchema { }
}