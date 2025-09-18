// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.Interfaces
{
	internal interface IProvider
	{
		List<IProvider> Children { get; }
	}

	internal interface ITokenProvider : IProvider
	{
		string TokenString { get; }
	}
	
	internal interface IInputProvider : IProvider
	{
		IInput Input { get; }
	}
	
	internal interface IParameterProvider : IProvider
	{
		List<IParameter> Parameters { get; }
	}
}