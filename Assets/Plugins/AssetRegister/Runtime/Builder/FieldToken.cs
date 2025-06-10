// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	internal class FieldToken : ITokenProvider
	{
		public List<IProvider> Children { get; } = new();
		public string TokenString { get; }
		
		public FieldToken(string fieldName)
		{
			TokenString = fieldName;
		}
	}
}