// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime;
using Plugins.AssetRegister.Runtime.Schema.Mutations;
using UnityEngine;

	public class Test : MonoBehaviour
	{
		public void Start()
		{
			var request = AR.NewMutationBuilder()
				.AddCreateNamespaceMutation("", "")
					.WithField(n => n.Id)
					.WithMethod(n => n.schemas(null, null, 0f, 0f))
						.WithField(s => s.Total)
						.Done()
					.Done()
				.Build();
		}
	}