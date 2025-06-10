// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Schema.Mutations;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
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
			
			var request = Runtime.AssetRegister.NewMutation()
				.Add(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
					.WithField(x => x.Asset.TokenId)
					.Done()
				.SetAuth(_siweToken)
				.Build();

			var response = await _client.SendRequest(request, cancellationTokenSource.Token);
			
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
			
			if (response.TryGetModel<UpdateAssetProfile>(out var updateAssetProfile))
			{
				Debug.Log(updateAssetProfile.Asset.TokenId);
			}
		}
	}
}