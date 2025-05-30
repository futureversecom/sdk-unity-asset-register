// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;

namespace Plugins.AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AssetRegisterQueryAttribute : Attribute
	{
		public readonly string QueryName;

		public AssetRegisterQueryAttribute(string queryName)
		{
			QueryName = queryName;
		}
	}
}