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
	#if UNITY_EDITOR
	using UnityEditor;

	[CustomEditor(typeof(QueryAsset))]
	public class QueryAssetEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			EditorGUILayout.Space(8f);
			if (GUILayout.Button("Run Query"))
			{
				if (target is QueryAsset queryAsset)
				{
					#if USING_UNITASK
					queryAsset.RunQuery();
					#else
					queryAsset.StartCoroutine(queryAsset.RunQuery());
					#endif
				}
			}
		}
	}
	#endif
	
	public class QueryAsset : MonoBehaviour
	{
		[SerializeField] private MonoClient _client;
		[SerializeField] private string _collectionId;
		[SerializeField] private string _tokenId;
		[SerializeField] private string _address;
		
#if USING_UNITASK
		public async void RunQuery()
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

			Debug.Log("Sending GraphQL Query");
	
			var response = await _client.SendRequest(request, cancellationToken: cancellationTokenSource.Token);
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
				return;
			}
#else
		public IEnumerator RunQuery()
		{
			Debug.Log("Sending GraphQL Query");
			
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
				Debug.Log($"Asset Token ID is {asset.TokenId}");
				if (asset.Ownership is SFTAssetOwnership ownership)
				{
					Debug.Log($"Ownership is SFTAssetOwnership, balanceOf is {ownership.BalanceOf.Balance}");
				}
			}
		}
	}
}