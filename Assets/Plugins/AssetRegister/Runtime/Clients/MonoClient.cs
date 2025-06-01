// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugins.AssetRegister.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.AssetRegister.Runtime.Clients
{
	public class MonoClient : MonoBehaviour, IAssetRegisterClient
	{
		[SerializeField] private string _graphQlEndpoint;
		[SerializeField] private string _authenticationToken;

		public async UniTask<QueryResult<TModel>> Query<TModel, TInput>(
			QueryObject<TModel, TInput> query,
			string authenticationToken = null,
			CancellationToken cancellationToken = default)
			where TModel : class, IModel where TInput : class, IQueryVariables
		{
			using var webRequest = new UnityWebRequest(_graphQlEndpoint, "POST");
			var jsonPayload = JsonConvert.SerializeObject(
				new
				{
					query = query.QueryString,
					variables = query.Variables,
				},
				Formatting.None
			);
			
			webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload));
			webRequest.downloadHandler = new DownloadHandlerBuffer();
			
			var authToken = authenticationToken ?? _authenticationToken;
			webRequest.SetRequestHeader("Authorization", authToken);
			webRequest.SetRequestHeader("Content-Type", "application/json");

			await webRequest.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				return new QueryResult<TModel>(null, webRequest.error);
			}

			var resultString = webRequest.downloadHandler.text;
			return ParseResult<TModel>(resultString, query.QueryResponseName);
		}

		private static QueryResult<TModel> ParseResult<TModel>(string resultString, string queryName) where TModel : class, IModel
		{
			var json = JsonConvert.DeserializeObject<JObject>(resultString);
			var data = json["data"]?[queryName]?.ToObject<TModel>();
			var errors = json["errors"]?.ToObject<Error[]>();
			var error = errors != null ?
				$"GraphQL query returned errors: {string.Join(", ", errors.Select(e => e.Message))}" :
				null;
				
			return new QueryResult<TModel>(data, error);
		}
	}
}