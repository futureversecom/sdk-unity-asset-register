// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Clients;
using AssetRegister.Runtime.Schema.Mutations;
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

	[CustomEditor(typeof(SetAssetProfile))]
	public class SetAssetProfileEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			EditorGUILayout.Space(8f);
			if (GUILayout.Button("Run Mutation"))
			{
				if (target is SetAssetProfile setAssetProfile)
				{
#if USING_UNITASK
					setAssetProfile.RunMutation();
#else
					setAssetProfile.StartCoroutine(setAssetProfile.RunMutation());
#endif
				}
			}
		}
	}
#endif
	
	public class SetAssetProfile : MonoBehaviour
	{
		[SerializeField] private MonoClient _client;
		[SerializeField] private string _assetId;
		[SerializeField] private string _key;
		[SerializeField] private string _assetProfileUrl;
		[SerializeField] private string _authToken;

#if USING_UNITASK
		public async void RunMutation()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			
			var request = Runtime.AssetRegister.NewMutation()
				.Add(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
					.WithField(x => x.Asset.TokenId)
					.Done()
				.SetAuth(_authToken)
				.Build();

			Debug.Log("Sending GraphQL Mutation");

			var response = await _client.SendRequest(request, cancellationTokenSource.Token);
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
				return;
			}
#else
		public IEnumerator RunMutation()
		{
			Debug.Log("Sending GraphQL Mutation");
			
			IResponse response = null;
			yield return Runtime.AssetRegister.NewMutation()
				.Add(new UpdateAssetProfileMutation(_assetId, _key, _assetProfileUrl))
					.WithField(x => x.Asset.TokenId)
					.Done()
				.SetAuth(_authToken)
				.Execute(_client, callback: r => response = r);
			
			if (!response.Success)
			{
				Debug.LogError($"Errors in request: {response.Error}");
				yield break;
			}
#endif
			
			if (response.TryGetModel<UpdateAssetProfile>(out var updateAssetProfile))
			{
				Debug.Log($"Updated asset's Token ID is {updateAssetProfile.Asset.TokenId}");
			}
		}
	}
}