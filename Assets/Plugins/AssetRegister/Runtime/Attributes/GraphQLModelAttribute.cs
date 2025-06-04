// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;

namespace AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class GraphQLModelAttribute : Attribute
	{
		public readonly string ResponseName;

		public GraphQLModelAttribute(string responseName)
		{
			this.ResponseName = responseName;
		}
	}
}