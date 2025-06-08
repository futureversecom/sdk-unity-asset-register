// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IToken
	{
		string Serialize();
	}

	public interface IParameter
	{
		string ParameterName { get; }
		string ParameterType { get; }
	}
}