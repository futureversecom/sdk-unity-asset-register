// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using Plugins.AssetRegister.Runtime;
using Plugins.AssetRegister.Runtime.Clients;
using Plugins.AssetRegister.Runtime.SchemaObjects;
using UnityEngine;

namespace Plugins.AssetRegister.Samples.AssetSample
{
	public class QueryAsset : MonoBehaviour
	{
		[SerializeField] private MonoClient _client;
		[SerializeField] private string _collectionId;
		[SerializeField] private string _tokenId;
		
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();

			var result = await AssetRegisterQuery.Asset(_collectionId, _tokenId)
				.AddField(x => x.Collection.ChainID)
				.AddField(x => x.TokenId)
				.Execute(_client, cancellationToken: cancellationTokenSource.Token);
			
			if (!result.Success)
			{
				Debug.LogError($"Errors in request: {result.Error}");
			}
			
			Debug.Log(result.Data.TokenId);
			Debug.Log(result.Data.Collection.ChainID);
		}
	}
}