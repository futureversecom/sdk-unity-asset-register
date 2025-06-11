// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;

namespace AssetRegister.Runtime.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	internal abstract class GraphQLTypeAttribute : Attribute
	{
		public abstract string TypeName { get; }
	}
	
	internal class CustomTypeAttribute : GraphQLTypeAttribute
	{
		public override string TypeName { get; }

		public CustomTypeAttribute(string typeName)
		{
			TypeName = typeName;
		}
	}
	
	internal class StringAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "String";
	}
	
	internal class CollectionIdAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "CollectionId";
	}
	
	internal class UrlAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Url";
	}
	
	internal class ChainAddressAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "ChainAddress";
	}
	
	internal class ProfilesAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Profiles";
	}
}