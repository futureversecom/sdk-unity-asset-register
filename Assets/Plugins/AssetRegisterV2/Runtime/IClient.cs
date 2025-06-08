// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Plugins.AssetRegisterV2.Runtime
{
	public interface IClient
	{
		UniTask<IResponse> MakeRequest(IRequest request, string authToken, CancellationToken cancellationToken);
	}
}