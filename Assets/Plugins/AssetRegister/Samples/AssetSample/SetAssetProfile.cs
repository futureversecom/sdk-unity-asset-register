// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.RequestBuilder;
using AssetRegister.Runtime.Objects.Models;
using AssetRegister.Runtime.Objects.Mutations;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
using AssetRegister.Runtime.Helpers;

#else
using System.Collections;
#endif

namespace Plugins.AssetRegister.Samples.AssetSample
{
	public class SetAssetProfile : MonoBehaviour
	{
		[SerializeField] private MonoClient _client;
		[SerializeField] private string _assetId;
		[SerializeField] private string _key;
		[SerializeField] private string _assetProfileUrl;
		[SerializeField] private string _siweToken;

#if USING_UNITASK
		private async void Start()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			
			// Option 1
			var request = RequestBuilder.BeginMutation()
				.Add(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
					.WithField(x => x.CollectionId)
					.Done()
				.Build();

			var response = await _client.SendRequest(request, _siweToken, cancellationTokenSource.Token);
		
			// Option 2
			var response2 = await Mutation.UpdateAssetProfile(_assetId, _key, _assetProfileUrl)
				.WithField(x => x.CollectionId)
				.Execute(_client, _siweToken, cancellationTokenSource.Token);
			
			
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
				return;
			}
#else
		private IEnumerator Start()
		{
			IResponse response = null;
			yield return RequestBuilder.Mutation().Add(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
				.WithField(x => x.TokenId)
				.WithField(x => x.Collection.ChainID)
				.Execute(_client, callback: r => response = r);
			
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
				yield break;
			}
#endif
			
			if (response.TryGetModel<AssetModel>(out var asset))
			{
				Debug.Log(asset.CollectionId);
				Debug.Log(asset.TokenId);
			}
		}
	}
}