// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.RequestBuilder
{
	public class FieldTreeNode
	{
		internal readonly string FieldName;
		internal readonly List<FieldTreeNode> Children;

		internal FieldTreeNode(string fieldName)
		{
			FieldName = fieldName;
			Children = new List<FieldTreeNode>();
		}
	}
}