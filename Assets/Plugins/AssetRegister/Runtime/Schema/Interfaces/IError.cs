// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Schema.Interfaces
{
	public interface IError : IInterface
	{
		string Message { get; }
	}
}