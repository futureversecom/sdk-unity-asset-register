// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IQuery<out TFields, out TInput>
		where TFields : class, IModel
		where TInput : class, IInput
	{
		TInput Input { get; }
	}
}