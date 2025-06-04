// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System;
using System.Collections;
#endif

namespace AssetRegister.Runtime.Interfaces
{
	public interface IClient
	{
#if USING_UNITASK
		UniTask<IResponse>
#else
		IEnumerator
#endif
		SendRequest(
			IRequest request,
			string authenticationToken = null,
#if USING_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<IResponse> onComplete = null
#endif
		);
	}
}