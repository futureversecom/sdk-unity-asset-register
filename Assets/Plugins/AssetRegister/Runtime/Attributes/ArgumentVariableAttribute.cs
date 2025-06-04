// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	internal class ArgumentVariableAttribute : Attribute
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