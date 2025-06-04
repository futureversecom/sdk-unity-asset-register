// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.RequestBuilder;
using AssetRegister.Runtime.Objects.Models;
using AssetRegister.Runtime.Objects.Mutations;
using UnityEngine;

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
			var request = RequestBuilder.Mutation()
				.AddMutation(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
				.WithField(x => x.CollectionId)
				.Build();

			var response = await _client.SendRequest(request, _siweToken, cancellationTokenSource.Token);
#else
		private IEnumerator Start()
		{
			QueryResult<Asset> result = null;
			yield return AssetRegisterQuery.Asset(_collectionId, _tokenId)
				.AddField(x => x.TokenId)
				.AddField(x => x.Collection.ChainID)
				.Execute(_client, onComplete: r => result = r);
#endif

			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
#if USING_UNITASK
				return;
#else
				yield break;
#endif
			}

			if (response.TryGetModel<AssetModel>(out var asset))
			{
				Debug.Log(asset.CollectionId);
				Debug.Log(asset.TokenId);
			}
		}
	}
}