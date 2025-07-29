// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Text;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
#if USING_UNITASK && !AR_SDK_NO_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else 
using System.Collections;
#endif

namespace AssetRegister.Runtime.Clients
{
	public sealed class MonoClient : MonoBehaviour, IClient
	{
		public enum Environment
		{
			Staging,
			Production,
		}
		
		[SerializeField] private Environment _environment;

		private string _graphQLEndpoint;

		private void Awake()
		{
			SetEnvironment(_environment);
		}

		public void SetEnvironment(Environment environment)
		{
			var target = environment switch
			{
				Environment.Production => "app",
				Environment.Staging => "cloud",
				_ => throw new Exception("Environment not handled"),
			};
			_graphQLEndpoint = $"https://ar-api.futureverse.{target}/graphql";
		}

#if USING_UNITASK && !AR_SDK_NO_UNITASK
		public async UniTask<IResponse> 
#else
		public IEnumerator
#endif
		SendRequest(
			IRequest request,
#if USING_UNITASK && !AR_SDK_NO_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<IResponse> callback = null
#endif
		)
		{
			using var webRequest = new UnityWebRequest(_graphQLEndpoint, "POST");
			var jsonPayload = request.Serialize();
			
			webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload));
			webRequest.downloadHandler = new DownloadHandlerBuffer();
			foreach (var header in request.Headers)
			{
				webRequest.SetRequestHeader(header.Key, header.Value);
			}

#if USING_UNITASK && !AR_SDK_NO_UNITASK
			await webRequest.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);
#else
			yield return webRequest.SendWebRequest();
#endif

			if (webRequest.result != UnityWebRequest.Result.Success)
			{
#if USING_UNITASK && !AR_SDK_NO_UNITASK
				return new Response(null, webRequest.error);
#else
				callback?.Invoke(new Response(null, webRequest.error));
				yield break;
#endif
			}

			var resultString = webRequest.downloadHandler.text;
			var response = Response.Parse(resultString);
#if USING_UNITASK && !AR_SDK_NO_UNITASK
			return response;
#else
			callback?.Invoke(response);
#endif
		}
	}
}