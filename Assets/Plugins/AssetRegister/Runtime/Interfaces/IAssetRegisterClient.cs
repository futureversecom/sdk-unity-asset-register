// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Plugins.AssetRegister.Runtime.Interfaces
{
	public interface IAssetRegisterClient
	{
		UniTask<QueryResult<TModel>> Query<TModel, TInput>(
			QueryObject<TModel, TInput> query,
			string authenticationToken = null,
			CancellationToken cancellationToken = default)
			where TModel : class, IModel where TInput : class, IQueryVariables;
	}
}