// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

#if USING_UNITASK && !AR_SDK_NO_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System;
using System.Collections;
#endif

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Responsible for sending the GraphQL http request based on an IRequest object
	/// </summary>
	public interface IClient
	{
#if USING_UNITASK && !AR_SDK_NO_UNITASK
		/// <summary>
		/// Sends the http request and produces a response
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>The response object</returns>
		UniTask<IResponse>
#else
		/// <summary>
		/// Sends the http request and produces a response
		/// </summary>
		/// <param name="request"></param>
		/// <param name="callback">Callback containing the response object</param>
		/// <returns>The IEnumerator to yield on</returns>
		IEnumerator
#endif
		SendRequest(
			IRequest request,
#if USING_UNITASK && !AR_SDK_NO_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<IResponse> callback = null
#endif
		);
	}
}