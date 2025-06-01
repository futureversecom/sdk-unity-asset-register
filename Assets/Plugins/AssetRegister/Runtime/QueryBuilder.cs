// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;
using UnityEngine;

namespace Plugins.AssetRegister.Runtime
{
	public class QueryBuilder<TModel, TVariables> where TModel: class, IModel where TVariables : class, IQueryVariables
	{
		private class FieldTreeNode
		{
			public readonly string FieldName;
			public readonly List<FieldTreeNode> Children;

			public FieldTreeNode(string fieldName)
			{
				FieldName = fieldName;
				Children = new List<FieldTreeNode>();
			}

			public void AddChild(FieldTreeNode child)
			{
				Children.Add(child);
			}
		}

		private struct ParameterInfo
		{
			public string ParameterName;
			public string ParameterType;
		}
		
		private readonly string _queryName;
		private readonly List<ParameterInfo> _parameters = new();
		private readonly List<FieldTreeNode> _baseFieldTreeNodes = new();
		private TVariables _queryInput;

		public QueryBuilder()
		{
			var modelType = typeof(TModel);
			var modelAttribute = modelType.GetCustomAttribute<QueryModelAttribute>();
			if (modelAttribute == null)
			{
				Debug.LogError("QueryBuilder error - TModel missing QueryModelAttribute");
				return;
			}
			
			_queryName = modelType.Name;
			
			var type = typeof(TVariables);
			var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

			foreach (var member in members)
			{
				if (member is not FieldInfo field)
				{
					continue;
				}

				if (TryGetParameterInfoFromField(field, out var parameterInfo))
				{
					_parameters.Add(parameterInfo);
				}
			}
		}

		public QueryBuilder<TModel, TVariables> SetVariables(TVariables input)
		{
			_queryInput = input;
			return this;
		}

		private static bool TryGetParameterInfoFromField(FieldInfo field, out ParameterInfo parameterInfo)
		{
			var jsonAttribute = field.GetCustomAttribute<JsonPropertyAttribute>();
			if (jsonAttribute == null)
			{
				parameterInfo = default(ParameterInfo);
				return false;
			}
				
			var scalarAttribute = field.GetCustomAttribute<QueryInputVariableAttribute>();
			
			var parameterName = jsonAttribute.PropertyName;
			var typeName = scalarAttribute == null ? field.FieldType.Name : scalarAttribute.ScalarTypeType.ToString();
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

		public QueryBuilder<TModel, TVariables> AddField<TField>(Expression<Func<TModel, TField>> fieldExpression)
		{
			// Push to stack to get the member chain in the reverse order
			var currentExpression = fieldExpression.Body;
			var expressionStack = new Stack<MemberExpression>();
			while (currentExpression != null && currentExpression.NodeType != ExpressionType.Parameter)
			{
				var memberExpression = (MemberExpression)currentExpression;
				expressionStack.Push(memberExpression);
				currentExpression = memberExpression.Expression;
			}

			// Unwind stack and add property names to the tree
			var currentNodes = _baseFieldTreeNodes;
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
			
			return this;
		}

		public QueryObject<TModel, TVariables> Build()
		{
			var queryString = new StringBuilder();
			queryString.Append("query (");
			queryString.Append(string.Join(", ", _parameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			queryString.AppendLine(") {");
			
			queryString.Append($"{_queryName}(");
			queryString.Append(string.Join(", ", _parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}")));
			queryString.AppendLine("){");

			BuildFieldsStringRecursive(ref queryString, _baseFieldTreeNodes);
			
			queryString.AppendLine("}");
			queryString.Append("}");
			
			return new QueryObject<TModel, TVariables>(_queryName, queryString.ToString(), _queryInput);
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

		public async UniTask<QueryResult<TModel>> Execute(
			IAssetRegisterClient client,
			string authenticationToken = null,
			CancellationToken cancellationToken = default)
		{
			var queryObject = Build();
			return await client.Query(queryObject, authenticationToken, cancellationToken:cancellationToken);
		}
	}
}