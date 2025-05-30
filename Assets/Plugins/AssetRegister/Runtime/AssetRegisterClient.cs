// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.AssetRegister.Runtime
{
	public class AssetRegisterClient : MonoBehaviour, IAssetRegisterClient
	{
		[SerializeField] private string _graphQlEndpoint;

		public async UniTask<Result<TModel>> Query<TModel>(
			string queryName,
			string queryString,
			CancellationToken cancellationToken) where TModel : class, IModel
		{
			var jsonPayload = JsonConvert.SerializeObject(new { query = queryString }, Formatting.None);
			using var webRequest = new UnityWebRequest(_graphQlEndpoint, "POST");
          
			webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload));
			webRequest.downloadHandler = new DownloadHandlerBuffer();
			webRequest.SetRequestHeader("Content-Type", "application/json");

			await webRequest.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				return new Result<TModel>(null, webRequest.error);
			}

			var resultString = webRequest.downloadHandler.text;
			var json = JsonConvert.DeserializeObject<JObject>(resultString);
			var data = json["data"]?[queryName]?.ToObject<TModel>();
			var error = GetError(json);
				
			return new Result<TModel>(data, error);
		}

		private string GetError(JObject result)
		{
			var errors = result["errors"]?.ToObject<AssetRegisterError[]>();
			return errors != null ?
				$"GraphQL query returned errors: {string.Join(", ", errors.Select(e => e.Message))}" :
				null;
		}
	}
}