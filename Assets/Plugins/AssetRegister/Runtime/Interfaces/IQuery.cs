// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IQuery<out TFields, out TArguments>
		where TFields : class, IModel
		where TArguments : class, IInput
	{
		TArguments Arguments { get; }
	}
}