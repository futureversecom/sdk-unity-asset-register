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
using AssetRegister.Runtime.Interfaces;
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
				.Add(new AssetQuery(_collectionId, _tokenId))
					.WithField(a => a.CollectionId)
					.WithField(a => a.TokenId)
					.Done()
				// .Add(new NamespaceQuery(""))
				// 	.WithField(n => n.Id)
				// 	.WithField(n => n.Url)
				// 	.Done()
				.Build();

			var result = await _client.SendRequest(request, cancellationToken: cancellationTokenSource.Token);
		
			if (!result.Success)
			{
				Debug.LogError($"Errors in request: {result.Error}");
				return;
			}
#else
		private IEnumerator Start()
		{
			IResponse result = null;
			yield return RequestBuilder.Query()
				.Add(new AssetQuery(_collectionId, _tokenId))
				.WithField(x => x.TokenId)
				.WithField(x => x.CollectionId)
				.Execute(_client, callback:r => result = r);
			
			if (!result.Success)
			{
				Debug.LogError($"Errors in request: {result.Error}");
				yield break;
			}
#endif
			
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