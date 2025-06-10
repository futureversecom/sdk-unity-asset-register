// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	public class FieldBuilder : ITokenProvider
	{
		public List<IProvider> Children { get; } = new();
		public string TokenString { get; }
		
		public FieldBuilder(string fieldName)
		{
			TokenString = fieldName;
		}
	}
}