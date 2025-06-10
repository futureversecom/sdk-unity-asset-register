// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;
using AssetRegister.Runtime.Schema.Unions;
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
		[SerializeField] private string _address;
		
#if USING_UNITASK
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			
			// Option 1
			var request = Runtime.AssetRegister.NewQuery()
				.Add(new AssetQuery(_collectionId, _tokenId))
					.WithField(a => a.TokenId)
					.WithField(a => a.Collection.ChainID)
					.WithUnion(a => a.Ownership)
						.On<NFTAssetOwnership>()
							.WithField(nft => nft.Owner.Handle)
							.Done()
						.On<SFTAssetOwnership>()
							.WithMethod(sft => sft.balanceOf(_address))
								.WithField(b => b.Balance)
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
			IResponse response = null;
			yield return Runtime.AssetRegister.NewQuery()
				.Add(new AssetQuery(_collectionId, _tokenId))
					.WithField(a => a.TokenId)
					.WithField(a => a.Collection.ChainID)
					.WithUnion(a => a.Ownership)
						.On<NFTAssetOwnership>()
							.WithField(nft => nft.Owner.Handle)
							.Done()
						.On<SFTAssetOwnership>()
							.WithMethod(sft => sft.balanceOf(_address))
								.WithField(b => b.Balance)
				.Execute(_client, callback:r => response = r);
			
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
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
		}
	}
}