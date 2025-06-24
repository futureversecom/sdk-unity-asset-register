// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Unions;
using Plugins.AssetRegister.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

	public class Test : MonoBehaviour
	{
		public void Start()
		{
			var request = AR.NewQueryBuilder()
				.AddAssetsQuery()
					.OnArray<AssetEdge, AssetEdge[]>(a => a.Edges)
						.OnMember(e => e.Node)
							.WithField(n => n.Id)
							.WithField(n => n.CollectionId)
							.WithField(n => n.TokenId)
							.WithField(n => n.AssetType)
							.OnMember(n => n.Metadata)
								.WithField(m => m.Properties)
								.WithField(m => m.Uri)
								.WithField(m => m.Attributes)
								.WithField(m => m.RawAttributes)
								.Done()
							.OnMember(n => n.Collection)
								.WithField(c => c.ChainID)
								.WithField(c => c.ChainType)
								.Done()
							.OnUnion(n => n.Links)
								.On<NFTAssetLink>()
									.OnArray<Link, Link[]>(nft => nft.ChildLinks)
										.WithField(l => l.Asset.Id)
										.WithField(l => l.Path)
				.Build();
		}
	}