// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegisterV2.Runtime.Schemas;
using UnityEngine;

namespace Plugins.AssetRegisterV2.Runtime
{
	public class QueryTest : MonoBehaviour
	{
		public void Start()
		{
			var request = new QueryBuilder()
				.Add<Asset>()
					.WithField(a => a.Id)
					.WithField(a => a.Account.Id)
					.WithUnion(a => a.Ownership)
						.As<SFTAssetOwnership>()
							.WithField(sft => sft.Id)
							.Done()
						.As<NFTAssetOwnership>()
							.WithField(nft => nft.Id)
				.Build();
		}
	}
}