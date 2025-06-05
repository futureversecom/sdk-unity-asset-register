// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Attributes;
using Newtonsoft.Json;
using UnityEngine;

namespace AssetRegister.Runtime.RequestBuilder
{
	internal static class BuilderUtils
	{
		private static bool TryGetParameterInfoFromField(FieldInfo field, out ParameterInfo parameterInfo)
		{
			var jsonAttribute = field.GetCustomAttribute<JsonPropertyAttribute>();
			if (jsonAttribute == null)
			{
				parameterInfo = default(ParameterInfo);
				return false;
			}
				
			var scalarAttribute = field.GetCustomAttribute<ArgumentVariableAttribute>();
			
			var parameterName = jsonAttribute.PropertyName;
			var typeName = scalarAttribute?.TypeName ?? field.FieldType.Name;
			var required = scalarAttribute?.Required ?? false;

			if (field.FieldType.IsArray)
			{
				typeName = $"[{typeName}]";
			}
			if (required)
			{
				typeName += "!";
			}
			
			parameterInfo = new ParameterInfo()
			{
				ParameterName = parameterName,
				ParameterType = typeName,
			};
			return true;
		}

		public static void PopulateFieldTree<TModel, TField>(Expression currentExpression, ref FieldTreeNode rootNode)
		{
			// Push to stack to get the member chain in the reverse order
			var expressionStack = new Stack<MemberExpression>();
			while (currentExpression != null && currentExpression.NodeType != ExpressionType.Parameter)
			{
				var memberExpression = (MemberExpression)currentExpression;
				expressionStack.Push(memberExpression);
				currentExpression = memberExpression.Expression;
			}

			// Unwind stack and add property names to the tree
			var currentNodes = rootNode.Children;
			while (expressionStack.TryPop(out var expression))
			{
				var member = expression.Member;
			
				var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
				if (jsonProperty == null)
				{
					break;
				}

				var name = jsonProperty.PropertyName;
				var existingNode = currentNodes.FirstOrDefault(n => n.FieldName == name);
				if (existingNode == null)
				{
					existingNode = new FieldTreeNode(name);
					currentNodes.Add(existingNode);
				}

				currentNodes = existingNode.Children;
			}
		}
		
		public static string BuildModelString(IData data, bool includeParams)
		{
			var stringBuilder = new StringBuilder();
			BuildFieldsStringRecursive(ref stringBuilder, data.RootNode, includeParams ? data.Parameters : null);
			return stringBuilder.ToString();
		}
		
		private static void BuildFieldsStringRecursive(ref StringBuilder builder, FieldTreeNode fieldNode, List<ParameterInfo> parameters = null)
		{
			builder.AppendLine(fieldNode.FieldName);

			if (parameters != null)
			{
				builder.Append("(");
				builder.Append(
					string.Join(",", parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"))
				);
				builder.Append(")");
			}
			
			if (fieldNode.Children.Count == 0)
			{
				return;
			}

			builder.AppendLine("{");
			foreach (var child in fieldNode.Children)
			{
				BuildFieldsStringRecursive(ref builder, child);
			}
			builder.AppendLine("}");
		}
		
		public static List<ParameterInfo> ParametersFromType<TInput>() where TInput : IInput
		{
			var type = typeof(TInput);
			var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

			var list = new List<ParameterInfo>();
			foreach (var member in members)
			{
				if (member is not FieldInfo field)
				{
					continue;
				}

				if (TryGetParameterInfoFromField(field, out var parameterInfo))
				{
					list.Add(parameterInfo);
				}
			}

			return list;
		}

		public static FieldTreeNode RootNodeFromModel<TModel>() where TModel : IModel
		{
			var modelType = typeof(TModel);
			var modelAttribute = modelType.GetCustomAttribute<GraphQLModelAttribute>();
			if (modelAttribute == null)
			{
				Debug.LogError("QueryBuilder error - TModel missing QueryModelAttribute");
				return null;
			}
			
			var queryName = modelAttribute.ResponseName;
			return new FieldTreeNode(queryName);
		}
	}
}