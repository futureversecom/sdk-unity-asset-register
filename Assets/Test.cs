// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Schema.Objects;
using Plugins.AssetRegister.Runtime;
using UnityEngine;

	public class Test : MonoBehaviour
	{
		public void Start()
		{
			var request = AR.NewQueryBuilder()
				.AddAssetsQuery(collectionIds:new string[] {""})
					.WithArray<AssetEdge, AssetEdge[]>(a => a.Edges)
						.WithField(e => e.Node)	
				.Build();
		}
	}