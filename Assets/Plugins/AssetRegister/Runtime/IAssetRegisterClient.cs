// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegister.Runtime.Models;

namespace Plugins.AssetRegister.Runtime
{
	public interface IAssetRegisterClient
	{
		UniTask<Result<T>> Query<T>(string queryName, string queryString, CancellationToken cancellationToken)
			where T : class, IModel;
	}
}