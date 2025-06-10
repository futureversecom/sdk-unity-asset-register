// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Represents a single GraphQL mutation. Can be added to a MutationBuilder. 
	/// </summary>
	/// <typeparam name="TModel">The type of model that is affected by the mutation</typeparam>
	/// <typeparam name="TInput">The type of input that is required by the mutation</typeparam>
	public interface IMutation<out TModel, out TInput>
		where TModel : IModel
		where TInput : class, IInput
	{
		TInput Input { get; }
	}
}