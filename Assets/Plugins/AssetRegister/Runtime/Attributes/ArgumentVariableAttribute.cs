// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace Plugins.AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ArgumentVariableAttribute : Attribute
	{
		public readonly string TypeName;
		public readonly bool Required;

		public ArgumentVariableAttribute(bool required = false, string typeName = null)
		{
			TypeName = typeName;
			Required = required;
		}
	}
}