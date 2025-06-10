// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
using AssetRegister.Runtime.Schema.Unions;
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
		[SerializeField] private string _address;
		
#if USING_UNITASK
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			
			// Option 1
			var request = new QueryBuilder()
				.Add(new AssetQuery(_collectionId, _tokenId))
					.WithUnion(a => a.Ownership)
						.On<NFTAssetOwnership>()
							.WithField(nft => nft.Owner.Handle)
							.Done()
						.On<SFTAssetOwnership>()
							.WithMethod(sft => sft.balanceOf(_address))
								.WithField(b => b.Balance)
								.Done()
							.Done()
						.Done()
					.WithField(a => a.TokenId)
					.WithField(a => a.Collection.ChainID)
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
				Debug.Log(asset.TokenId);
				if (asset.Ownership is SFTAssetOwnership ownership)
				{
					Debug.Log(ownership.BalanceOf.Balance);
				}
			}
			
			if (response.TryGetModel<Namespace>(out var @namespace))
			{
				Debug.Log(@namespace.Id);
			}
		}
	}
}