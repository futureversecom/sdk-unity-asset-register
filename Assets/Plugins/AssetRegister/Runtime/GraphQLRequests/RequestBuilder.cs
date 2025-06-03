// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public abstract class RequestBuilder<TData> where TData : IQueryData
	{
		protected readonly List<TData> RequestData = new();

		internal RequestBuilder<TData> RegisterQuery(TData data)
		{
			RequestData.Add(data);
			return this;
		}

		public abstract GraphQLRequest Build();
		
		protected static string BuildModelString(TData data, bool includeParams)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(data.RootNode.FieldName);
			if (includeParams)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(
					string.Join(",", data.Parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"))
				);
				stringBuilder.Append(")");
			}
			stringBuilder.AppendLine("{");
			BuildFieldsStringRecursive(ref stringBuilder, data.RootNode.Children);
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		private static void BuildFieldsStringRecursive(ref StringBuilder builder, List<FieldTreeNode> fieldNodes)
		{
			foreach (var fieldNode in fieldNodes)
			{
				builder.AppendLine(fieldNode.FieldName);
				if (fieldNode.Children.Count == 0)
				{
					continue;
				}
				
				builder.AppendLine("{");
				BuildFieldsStringRecursive(ref builder, fieldNode.Children);
				builder.AppendLine("}");
			}
		}
	}
}