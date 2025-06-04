// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;
using Plugins.AssetRegister.Runtime.Requests;
using UnityEngine;
using UnityEngine.Networking;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else 
using System.Collections;
using System;
#endif

namespace Plugins.AssetRegister.Runtime.Clients
{
	public class MonoClient : MonoBehaviour, IAssetRegisterClient
	{
		[SerializeField] private string _graphQlEndpoint;
		[SerializeField] private string _authenticationToken;

#if USING_UNITASK
		public async UniTask<Result> 
#else
		public IEnumerator
#endif
		MakeRequest(
			Request request,
			string authenticationToken = null,
#if USING_UNITASK
			CancellationToken cancellationToken = default
#else
			Action<QueryResult> onComplete = null
#endif
		)
		{
			using var webRequest = new UnityWebRequest(_graphQlEndpoint, "POST");
			var jsonPayload = JsonConvert.SerializeObject(
				new
				{
					query = request.QueryString,
					variables = request.Args,
				},
				Formatting.None
			);
			
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
				return new Result(null, webRequest.error);
#else
				onComplete?.Invoke(new QueryResult<TModel>(null, webRequest.error));
				yield break;
#endif
			}

			var resultString = webRequest.downloadHandler.text;
			var result = Result.Parse(resultString);
#if USING_UNITASK
			return result;
#else
			onComplete?.Invoke(result);
#endif
		}
	}
}