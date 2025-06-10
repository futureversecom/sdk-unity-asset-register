// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.Interfaces
{
	public interface IRequest
	{
		Dictionary<string, string> Headers { get; }
		string Serialize();
		void OverrideArguments<T>(T arguments) where T : class, IInput;
	}
}