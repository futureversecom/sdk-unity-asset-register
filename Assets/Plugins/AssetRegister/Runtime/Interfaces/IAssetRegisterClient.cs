// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Requests;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System;
using System.Collections;
#endif

namespace Plugins.AssetRegister.Runtime.Interfaces
{
	public interface IAssetRegisterClient
	{
#if USING_UNITASK
		UniTask<Result>
#else
		IEnumerator
#endif
		MakeRequest(
			Request request,
			string authenticationToken = null,
#if USING_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<QueryResult> onComplete = null
#endif
		);
	}
}