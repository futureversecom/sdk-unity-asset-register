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
	
	internal class BooleanAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Boolean";
	}

	internal class FloatAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Float";
	}
	
	internal class SignatureAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "Signature";
	}
	
	internal class AssetTransactionMessageAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "AssetTransactionMessage";
	}
	
	internal class TransactionHashAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "TransactionHash";
	}
	
	internal class SchemaIdentifierAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "SchemaIdentifier";
	}
	
	internal class PropertyValueAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "PropertyValue";
	}
	
	internal class GenericTokenIdAttribute : GraphQLTypeAttribute
	{
		public override string TypeName => "GenericTokenId";
	}
}