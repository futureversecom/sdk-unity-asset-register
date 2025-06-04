// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Text;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else 
using System.Collections;
using System;
#endif

namespace AssetRegister.Runtime.Clients
{
	public sealed class MonoClient : MonoBehaviour, IClient
	{
		[SerializeField] private string _graphQlEndpoint;
		[SerializeField] private string _authenticationToken;

#if USING_UNITASK
		public async UniTask<IResponse> 
#else
		public IEnumerator
#endif
		SendRequest(
			IRequest request,
			string authenticationToken = null,
#if USING_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<IResponse> onComplete = null
#endif
		)
		{
			using var webRequest = new UnityWebRequest(_graphQlEndpoint, "POST");
			var jsonPayload = request.Serialize();
			
			webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload));
			webRequest.downloadHandler = new DownloadHandlerBuffer();
			
			var authToken = authenticationToken ?? _authenticationToken;
			webRequest.SetRequestHeader("Authorization", authToken);
			webRequest.SetRequestHeader("Content-Type", "application/json");

#if USING_UNITASK
			await webRequest.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);
#else
			yield return webRequest.SendWebRequest();
#endif

			if (webRequest.result != UnityWebRequest.Result.Success)
			{
#if USING_UNITASK
				return new Response(null, webRequest.error);
#else
				onComplete?.Invoke(new Response(null, webRequest.error));
				yield break;
#endif
			}

			var resultString = webRequest.downloadHandler.text;
			var result = Response.Parse(resultString);
#if USING_UNITASK
			return result;
#else
			onComplete?.Invoke(result);
#endif
		}
	}
}