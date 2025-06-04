// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Attributes;
using Plugins.AssetRegister.Runtime.Interfaces;
using Plugins.AssetRegister.Runtime.SchemaObjects;
using UnityEngine;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif

namespace Plugins.AssetRegister.Runtime.Requests
{
	public abstract class ASubBuilder<TModel, TArgs, TBuilder>: ISubBuilder<TModel, TArgs> 
		where TModel: class, IModel 
		where TArgs : class, IArguments
		where TBuilder : class, ISubBuilder<TModel, TArgs>
	{
		public FieldTreeNode RootNode { get; }
		public List<ParameterInfo> Parameters { get; } = new();
		public IArguments Args => _arguments;

		private TArgs _arguments;

		protected ASubBuilder()
		{
			var modelType = typeof(TModel);
			var modelAttribute = modelType.GetCustomAttribute<GraphQLModelAttribute>();
			if (modelAttribute == null)
			{
				Debug.LogError("QueryBuilder error - TModel missing QueryModelAttribute");
				return;
			}
			
			var queryName = modelAttribute.ResponseName;
			RootNode = new FieldTreeNode(queryName);
			
			var type = typeof(TArgs);
			var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

			foreach (var member in members)
			{
				if (member is not FieldInfo field)
				{
					continue;
				}

				if (TryGetParameterInfoFromField(field, out var parameterInfo))
				{
					Parameters.Add(parameterInfo);
				}
			}
		}

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

		public TBuilder WithArgs(TArgs input)
		{
			_arguments = input;
			return this as TBuilder;
		}

		public TBuilder WithField<TField>(Expression<Func<TModel, TField>> fieldExpression)
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
			var currentNodes = RootNode.Children;
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
			
			return this as TBuilder;
		}
	}
}