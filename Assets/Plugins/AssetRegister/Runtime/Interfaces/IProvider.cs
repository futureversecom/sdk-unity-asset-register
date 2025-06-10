// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.Interfaces
{
	public interface IProvider
	{
		List<IProvider> Children { get; }
	}

	public interface ITokenProvider : IProvider
	{
		string TokenString { get; }
	}
	
	public interface IInputProvider : IProvider
	{
		IInput Input { get; }
	}
	
	public interface IParameterProvider : IProvider
	{
		List<IParameter> Parameters { get; }
	}
}