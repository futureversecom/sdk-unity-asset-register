// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace AssetRegister.Runtime.Interfaces
{
	public interface IQuery<out TFields, out TArguments>
		where TFields : class, IModel
		where TArguments : class, IArgs
	{
		TArguments Arguments { get; }
	}
}