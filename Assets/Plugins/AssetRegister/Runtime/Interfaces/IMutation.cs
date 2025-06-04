// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace Plugins.AssetRegister.Runtime.Interfaces
{
	public interface IMutation<out TFields, TArguments>
		where TFields : class, IModel
		where TArguments : class, IArguments
	{
		string FunctionName { get; }
		TArguments Arguments { get; }
	}
}