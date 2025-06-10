// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	public class StringTokenProvider : ITokenProvider
	{
		public List<IProvider> Children => null;
		public string TokenString { get; }

		public StringTokenProvider(string tokenString)
		{
			TokenString = tokenString;
		}
	}
}