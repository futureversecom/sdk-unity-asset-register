// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IResponse
	{
		bool Success { get; }
		string Error { get; }
		bool TryGetModel<T>(out T model) where T : class, IModel;
	}
}