// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public interface ISubBuilder<TModel, TVariables> 
		where TModel: class, IModel 
		where TVariables : class, IArguments
	{
		
	}
}