// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Objects.Models;
using AssetRegister.Runtime.Objects.Queries;
using AssetRegister.Runtime.RequestBuilder;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
#else
using System.Collections;
#endif

namespace Plugins.AssetRegister.Samples.AssetSample
{
	public class QueryAsset : MonoBehaviour
	{
		[SerializeField] private MonoClient _client;
		[SerializeField] private string _collectionId;
		[SerializeField] private string _tokenId;
		[SerializeField] private string _namespace;
		
#if USING_UNITASK
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			var request = RequestBuilder.Query()
				.AddQuery(new AssetQuery(_collectionId, _tokenId))
					.WithField(a => a.CollectionId)
					.WithField(a => a.TokenId)
					.Done()
				// .AddQuery(new NamespaceQuery(_namespace))
				// 	.WithField(n => n.Id)
				// 	.Done()
				.Build();

			var result = await _client.SendRequest(request, cancellationToken: cancellationTokenSource.Token);
#else
		private IEnumerator Start()
		{
			QueryResult<Asset> result = null;
			yield return AssetRegisterQuery.Asset(_collectionId, _tokenId)
				.AddField(x => x.TokenId)
				.AddField(x => x.Collection.ChainID)
				.Execute(_client, onComplete: r => result = r);
#endif
			
			if (!result.Success)
			{
				Debug.LogError($"Errors in request: {result.Error}");
#if USING_UNITASK
				return;
#else
				yield break;
#endif
			}

			if (result.TryGetModel<AssetModel>(out var asset))
			{
				Debug.Log(asset.CollectionId);
				Debug.Log(asset.TokenId);
			}
			
			if (result.TryGetModel<NamespaceModel>(out var @namespace))
			{
				Debug.Log(@namespace.Id);
			}
		}
	}
}