// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Utils
{
	public static class BuilderUtils
	{
		internal static string GetSchemaName<TSchema>() where TSchema : ISchema
		{
			var type = typeof(TSchema);
			var attribute = type.GetCustomAttribute<JsonObjectAttribute>();
			return attribute == null ? type.Name : attribute.Id;
		}
		
		internal static object GetValueFromExpression(Expression expr)
		{
			switch (expr)
			{
				case ConstantExpression constExpr:
					return constExpr.Value;
				case MemberExpression memberExpr:
					var instance = memberExpr.Expression != null 
						? GetValueFromExpression(memberExpr.Expression)
						: null;
					return memberExpr.Member switch
					{
						FieldInfo field => field.GetValue(instance),
						PropertyInfo prop => prop.GetValue(instance),
						_ => throw new NotSupportedException($"Member {memberExpr.Member} not supported")
					};
				case UnaryExpression { NodeType: ExpressionType.Convert } unaryExpr:
					var operandValue = GetValueFromExpression(unaryExpr.Operand);
					var targetType = unaryExpr.Type;
					return Convert.ChangeType(operandValue, targetType);
				default:
					var lambda = Expression.Lambda(expr);
					var compiled = lambda.Compile();
					return compiled.DynamicInvoke();
			}
		}
		
		internal static object GetDefaultValue(Type type)
		{
			return type.IsValueType ? Activator.CreateInstance(type) : null;
		}
		
		internal static List<IParameter> CreateParametersFromInput<TInput>(TInput input) where TInput : class, IInput
        {
            var inputType = typeof(TInput);
            var fields = inputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var parameterList = new List<IParameter>();

            foreach (var field in fields)
            {
                var value = field.GetValue(input);
                if (value == null || value.Equals(BuilderUtils.GetDefaultValue(value.GetType())))
                {
                    continue;
                }
                
                var parameter = CreateParameterFromField(field);
                parameterList.Add(parameter);
            }

            return parameterList;
        }

		internal static ParameterData CreateParameterFromInfo(ParameterInfo paramInfo)
        {
            var typeAttribute = paramInfo.GetCustomAttribute<GraphQLTypeAttribute>();
            var requiredAttribute = paramInfo.GetCustomAttribute<RequiredAttribute>();

            var typeName = typeAttribute?.TypeName ?? paramInfo.ParameterType.Name;
            if (paramInfo.ParameterType.IsArray)
            {
                typeName = $"[{typeName}]";
            }
            if (requiredAttribute != null)
            {
                typeName += "!";
            }

            return new ParameterData(paramInfo.Name, typeName);
        }

		internal static ParameterData CreateParameterFromField(FieldInfo field)
        {
            var typeAttribute = field.GetCustomAttribute<GraphQLTypeAttribute>();
            var requiredAttribute = field.GetCustomAttribute<RequiredAttribute>();
            var jsonAttribute = field.GetCustomAttribute<JsonPropertyAttribute>();

            var typeName = typeAttribute?.TypeName ?? field.FieldType.Name;
            if (field.FieldType.IsArray)
            {
                typeName = $"[{typeName}!]";
            }
            if (requiredAttribute != null)
            {
                typeName += "!";
            }

            var name = jsonAttribute?.PropertyName ?? field.Name;
            return new ParameterData(name, typeName);
        }

		internal static string BuildTokenString(string methodName, List<IParameter> parameters)
        {
            var args = string.Join(", ", parameters.Select(GetParameterString));
            return args.Length == 0 ? methodName : $"{methodName} ({args})";
        }

		internal static string GetParameterString(IParameter parameter)
        {
            var name = parameter.ParameterName;
            if (name.EndsWith("_input"))
            {
                name = "input";
            }
            return $"{name}: ${parameter.ParameterName}";
        }
		
		internal static IProvider ProcessPath(Expression expression, ITokenProvider currentProvider)
		{
			var stack = new Stack<string>();
			while (expression != null && expression.NodeType != ExpressionType.Parameter)
			{
				if (expression is not MemberExpression memberExpression)
				{
					throw new ArgumentException("Non-member path provided");
				}
				
				var member = memberExpression.Member; 
				
				var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
				var name = jsonProperty?.PropertyName ?? member.Name;
				stack.Push(name);
				
				expression = memberExpression.Expression;
			}

			while (stack.TryPop(out var name))
			{
				var existingBuilder =
					currentProvider.Children.FirstOrDefault(c => c is ITokenProvider t && t.TokenString == name);
				if (existingBuilder == null)
				{
					existingBuilder = new FieldToken(name);
					currentProvider.Children.Add(existingBuilder);
				}

				currentProvider = (ITokenProvider)existingBuilder;
			}

			return currentProvider;
		}
		
		internal static bool IsMemberArray(MemberExpression memberExpression)
		{
			return memberExpression.Member switch
			{
				PropertyInfo propertyInfo => propertyInfo.PropertyType.IsArray,
				FieldInfo fieldInfo => fieldInfo.FieldType.IsArray,
				_ => false,
			};
		}
		
		internal static bool IsMemberUnion(MemberExpression memberExpression)
		{
			return memberExpression.Member switch
			{
				PropertyInfo propertyInfo => typeof(IUnion).IsAssignableFrom(propertyInfo.PropertyType),
				FieldInfo fieldInfo => typeof(IUnion).IsAssignableFrom(fieldInfo.FieldType),
				_ => false,
			};
		}
		
		internal static bool IsMemberInterface(MemberExpression memberExpression)
		{
			return memberExpression.Member switch
			{
				PropertyInfo propertyInfo => typeof(IInterface).IsAssignableFrom(propertyInfo.PropertyType) &&
					propertyInfo.PropertyType.IsInterface,
				FieldInfo fieldInfo => typeof(IInterface).IsAssignableFrom(fieldInfo.FieldType) &&
					fieldInfo.FieldType.IsInterface,
				_ => false,
			};
		}
	}
}