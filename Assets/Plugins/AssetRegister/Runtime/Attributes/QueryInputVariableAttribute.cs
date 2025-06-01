// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using Plugins.AssetRegister.Runtime.SchemaObjects;

namespace Plugins.AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class QueryInputVariableAttribute : Attribute
	{
		public readonly ScalarType ScalarTypeType;
		public readonly bool Required;

		public QueryInputVariableAttribute(bool required = false, ScalarType scalarTypeType = ScalarType.NonScalar)
		{
			ScalarTypeType = scalarTypeType;
			Required = required;
		}
	}
}