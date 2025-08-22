// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.Interfaces
{
	internal interface IProvider
	{
		List<IProvider> Children { get; }
		string TokenString { get; }
		IInput Input { get; }
		List<IParameter> Parameters { get; }
	}
}