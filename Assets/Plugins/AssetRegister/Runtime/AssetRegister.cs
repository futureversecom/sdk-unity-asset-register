// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;
using Newtonsoft.Json.Linq;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
#endif

namespace Plugins.AssetRegister.Runtime
{
	/// <summary>
	/// Main entry point of the plugin
	/// </summary>
	public static class AssetRegister
	{
		/// <summary>
		/// Generates a new query builder
		/// </summary>
		/// <returns></returns>
		public static IQueryBuilder NewQuery()
		{
			return new QueryBuilder();
		}
		
		/// <summary>
		/// Generates a new mutation builder
		/// </summary>
		/// <returns></returns>
		public static IMutationBuilder NewMutation()
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
			var result = await new QueryBuilder()
				.Add(new AssetQuery(collectionId, tokenId))
					.WithField(a => a.Profiles)
				.Execute(client, token);

			if (!result.Success)
			{
				Debug.Log(result.Error);
				return null;
			}

			if (!result.TryGetModel(out Asset asset))
			{
				Debug.Log("Couldn't get asset from result");
				return null;
			}

			if (!asset.Profiles.TryGetValue("asset-profile", out var profile))
			{
				Debug.Log("Profiles does not contain asset-profile");
				return null;
			}

			return profile.ToString();
		}
	}
}