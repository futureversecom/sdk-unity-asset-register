// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Represents a single GraphQL mutation. Can be added to a MutationBuilder. 
	/// </summary>
	/// <typeparam name="TSchema">The type of schema object that is affected by the mutation</typeparam>
	/// <typeparam name="TInput">The type of input that is required by the mutation</typeparam>
	public interface IMutation<out TSchema, out TInput>
		where TSchema : ISchema
		where TInput : class, IInput
	{
		string FunctionName { get; }
		TInput Input { get; }
	}
}