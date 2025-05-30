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
using Plugins.AssetRegister.Runtime.Models;
using UnityEngine;

namespace Plugins.AssetRegister.Runtime
{
	public class AssetRegisterQueryBuilder<T> where T: class, IModel
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
		
		private readonly string _queryName;
		private readonly Dictionary<string, object> _parameters = new();
		private readonly List<FieldTreeNode> _baseFieldTreeNodes = new();
		private readonly StringBuilder _queryString = new();

		internal static AssetRegisterQueryBuilder<T> FromMethod(MethodBase method, params object[] parameters)
		{
			if (method == null)
			{
				Debug.LogError("Method is null");
				return null;
			}
			
			var arQueryAttributes = method.GetCustomAttributes(typeof(AssetRegisterQueryAttribute));
			if (arQueryAttributes.FirstOrDefault() is not AssetRegisterQueryAttribute arQueryAttribute)
			{
				Debug.LogError("Method was missing AssetRegisterQueryAttribute");
				return null;
			}
			
			var qb = new AssetRegisterQueryBuilder<T>(arQueryAttribute.QueryName);
			var methodParameters = method.GetParameters();
			for (var i = 0; i < methodParameters.Length; i++)
			{
				_ = qb.SetParameter(methodParameters[i].Name, parameters[i]);
			}
			
			return qb;
		}

		public AssetRegisterQueryBuilder(string queryName)
		{
			_queryName = queryName;
		}

		public AssetRegisterQueryBuilder<T> SetParameter(string parameterName, object parameterValue)
		{
			_parameters.Add(parameterName, parameterValue);
			return this;
		}

		public AssetRegisterQueryBuilder<T> AddField<TField>(Expression<Func<T, TField>> fieldExpression)
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

		public string Build()
		{
			_queryString.AppendLine("query {");
			_queryString.Append($"{_queryName}(");

			var parameterStrings = string.Join(
				", ",
				_parameters.Select(GetParameterString)
			);
			_queryString.Append(parameterStrings);
			_queryString.AppendLine("){");

			AddFieldsToQuery(_baseFieldTreeNodes);
			
			_queryString.AppendLine("}");
			_queryString.Append("}");
			
			var queryString = _queryString.ToString();
			Debug.Log(queryString);
			return queryString;
		}

		private void AddFieldsToQuery(List<FieldTreeNode> fieldNodes)
		{
			foreach (var fieldNode in fieldNodes)
			{
				_queryString.AppendLine(fieldNode.FieldName);
				if (fieldNode.Children.Count == 0)
				{
					continue;
				}
				
				_queryString.AppendLine("{");
				AddFieldsToQuery(fieldNode.Children);
				_queryString.AppendLine("}");
			}
		}
		
		private static string GetParameterString(KeyValuePair<string, object> kvp)
		{
			return $"{kvp.Key}: \"{kvp.Value}\"";
		}
		
		public async UniTask<Result<T>> Execute(IAssetRegisterClient client, CancellationToken cancellationToken)
		{
			var queryString = Build();
			return await client.Query<T>(_queryName, queryString, cancellationToken);
		}
	}
}