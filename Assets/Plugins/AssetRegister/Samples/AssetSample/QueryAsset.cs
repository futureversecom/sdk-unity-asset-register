// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Reflection;
using System.Threading;
using Plugins.AssetRegister.Runtime;
using Plugins.AssetRegister.Runtime.Models;
using UnityEngine;

namespace Plugins.AssetRegister.Samples.AssetSample
{
	public class QueryAsset : MonoBehaviour
	{
		[SerializeField] private AssetRegisterClient _client;
		[SerializeField] private string _collectionId;
		[SerializeField] private string _tokenId;
		
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			
			var result = await Queries.AssetQuery(_collectionId, _tokenId)
				.AddField(x => x.TokenId)
				.AddField(x => x.Collection.ChainID)
				.Execute(_client, cancellationTokenSource.Token);
			
			if (result.Success)
			{
				Debug.Log(result.Data.TokenId);
				Debug.Log(result.Data.Collection.ChainID);
				return;
			}
			
			Debug.LogError($"Errors in request: {result.Error}");
		}
	}
}