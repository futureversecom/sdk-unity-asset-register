// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class FieldTreeNode
	{
		public readonly string FieldName;
		public readonly List<FieldTreeNode> Children;

		public FieldTreeNode(string fieldName)
		{
			FieldName = fieldName;
			Children = new List<FieldTreeNode>();
		}
	}
}