// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	internal class FieldToken : IProvider
	{
		public List<IProvider> Children { get; } = new();
		public string TokenString { get; }
		public IInput Input => null;
		public List<IParameter> Parameters => null;

		public FieldToken(string fieldName)
		{
			TokenString = fieldName;
		}
	}
}