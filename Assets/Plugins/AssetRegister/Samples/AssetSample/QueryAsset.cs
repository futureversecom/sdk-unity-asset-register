// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;
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
			
			// Option 1
			var request = new QueryBuilder()
				.Add(new AssetQuery(_collectionId, _tokenId))
				// 	.WithUnionField(a => a.Ownership)
				// 		.As((SFTAssetOwnership sft) => sft.Id)
				// 		.As((NFTAssetOwnership nft) => nft.Id)
				// 		.Done()
					.WithField(a => a.TokenId)
				// 	.Done()
				.Build();

			var response = await _client.SendRequest(request, cancellationToken: cancellationTokenSource.Token);
		
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
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
			
			if (response.TryGetModel<Asset>(out var asset))
			{
				Debug.Log(asset.CollectionId);
				Debug.Log(asset.TokenId);
			}
			
			if (response.TryGetModel<Namespace>(out var @namespace))
			{
				Debug.Log(@namespace.Id);
			}
		}
	}
}