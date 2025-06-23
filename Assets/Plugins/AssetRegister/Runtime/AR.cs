// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Queries;
using Newtonsoft.Json.Linq;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System;
using System.Collections;
#endif

namespace Plugins.AssetRegister.Runtime
{
	/// <summary>
	/// Main entry point of the plugin
	/// </summary>
	public static class AR
	{
		/// <summary>
		/// Generates a new query builder
		/// </summary>
		/// <returns></returns>
		public static IQueryBuilder NewQueryBuilder()
		{
			return new QueryBuilder();
		}
		
		/// <summary>
		/// Generates a new mutation builder
		/// </summary>
		/// <returns></returns>
		public static IMutationBuilder NewMutationBuilder()
		{
			return new MutationBuilder();
		}

		/// <summary>
		/// Create a custom GraphQL request
		/// </summary>
		/// <param name="body">The body of the GraphQL request</param>
		/// <param name="variables">Any variables that should be sent in the request</param>
		/// <param name="headers">Dictionary of headers that should be added to the http request (Content-Type is added automatically)</param>
		/// <returns>The request object</returns>
		public static IRequest RawRequest(string body, object variables = null, Dictionary<string, string> headers = null)
		{
			var headerDict = headers ?? new Dictionary<string, string>();
			headerDict.Add("Content-Type", "application/json");
			return new Request(
				body,
				variables == null ? new JObject() : JObject.FromObject(variables),
				headerDict
			);
		}
		
		#if USING_UNITASK
		/// <summary>
		/// Helper method to get the Asset Profile URL of a profile
		/// </summary>
		/// <param name="client"></param>
		/// <param name="collectionId"></param>
		/// <param name="tokenId"></param>
		/// <param name="token"></param>
		/// <returns>The profile URL, or null if an error occurred</returns>
		public static async UniTask<string> GetAssetProfileUrl(IClient client, string collectionId, string tokenId, CancellationToken token = default)
		{
			var response = await NewQueryBuilder()
				.AddAssetQuery(collectionId, tokenId)
					.WithField(a => a.Profiles)
				.Execute(client, token);
#else
		public static IEnumerator GetAssetProfileUrl(IClient client, string collectionId, string tokenId, Action<string> callback = null)
		{
			IResponse response = null;
			yield return NewQueryBuilder()
				.AddAssetQuery(collectionId, tokenId)
					.WithField((a => a.Profiles))
				.Execute(client, r => response = r);
#endif

			if (!response.Success)
			{
				Debug.LogError(response.Error);
#if USING_UNITASK
				return null;
#else
				callback?.Invoke(null);
				yield break;
#endif
			}

			if (!response.TryGetModel(out AssetResult assetResult))
			{
				Debug.LogError("Couldn't get asset from result");
#if USING_UNITASK
				return null;
#else
				callback?.Invoke(null);
				yield break;
#endif
			}

			var asset = assetResult.Asset;
			if (!asset.Profiles.TryGetValue("asset-profile", out var profile))
			{
				Debug.LogError("Profiles does not contain asset-profile");
#if USING_UNITASK
				return null;
#else
				callback?.Invoke(null);
				yield break;
#endif
			}

#if USING_UNITASK
			return profile.ToString();
#else
			callback?.Invoke(profile.ToString());
#endif
		}
	}
}