// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;

namespace AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public abstract class GraphQLTypeAttribute : Attribute
	{
		public abstract string TypeName { get; }
	}
	
	public class CustomTypeAttribute : GraphQLTypeAttribute
	{
		public override string TypeName { get; }

		public CustomTypeAttribute(string typeName)
		{
			TypeName = typeName;
		}
	}
	
	public class StringAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "String";
	}
	
	public class CollectionIdAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "CollectionId";
	}
	
	public class UrlAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Url";
	}
	
	public class ChainAddressAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "ChainAddress";
	}
}