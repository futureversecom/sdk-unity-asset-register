// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	public class Fragment<TModel> : IToken
	{
		public string Id { get; }

		public string Serialize()
			=> throw new System.NotImplementedException();
	}
}