// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IMutation<out TModel, out TInput>
		where TModel : IModel
		where TInput : class, IInput
	{
		TInput Input { get; }
	}
}