// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	public class FieldTreeBuilder : IToken
	{
		private readonly string _fieldName;
		private readonly List<FieldTreeBuilder> _children = new();

		public FieldTreeBuilder(string fieldName)
		{
			_fieldName = fieldName;
		}

		public FieldTreeBuilder WithPath(Stack<string> path)
		{
			if (!path.TryPop(out var pathPart))
			{
				return this;
			}

			var childBuilder = _children.FirstOrDefault(c => c._fieldName == pathPart);
			if (childBuilder != null)
			{
				childBuilder.WithPath(path);
			}
			else
			{
				var newBuilder = new FieldTreeBuilder(pathPart).WithPath(path);
				_children.Add(newBuilder);
			}
			
			return this;
		}

		public string Serialize()
		{
			var builder = new StringBuilder();
			if (_children.Count > 0)
			{
				builder.AppendLine($"{_fieldName} {{");
				foreach (var child in _children)
				{
					builder.Append(child.Serialize());
				}
				builder.AppendLine("}");
			}
			else
			{
				builder.AppendLine(_fieldName);
			}
			return builder.ToString();
		}
	}
}