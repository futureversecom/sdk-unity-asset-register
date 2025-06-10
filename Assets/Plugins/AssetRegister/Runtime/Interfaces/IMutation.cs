// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IMutation<out TFields, out TInput>
		where TFields : class, IModel
		where TInput : class, IInput
	{
		string FunctionName { get; }
		TInput Arguments { get; }
	}
}