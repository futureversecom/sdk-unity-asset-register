// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Represents a single GraphQL query. Can be added to a QueryBuilder
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	public interface IQuery<out TModel, out TInput>
		where TModel : IModel
		where TInput : class, IInput
	{
		TInput Input { get; }
	}
}