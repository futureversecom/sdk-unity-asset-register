// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Utils
{
	public static class Utils
	{
		public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attribute)
		where TAttribute : Attribute
		{
			attribute = type.GetCustomAttribute<TAttribute>();
			return attribute != null;
		}
		
		public static string GetSchemaName<TSchema>() where TSchema : ISchema
		{
			var type = typeof(TSchema);
			if (type.TryGetAttribute(out JsonObjectAttribute jsonObjectAttribute))
			{
				return jsonObjectAttribute.Id;
			}
			
			return type.Name.ToLower();
		}

		public static string GetGraphQLType(Type type)
		{
			var typeAttribute = type.GetCustomAttribute<GraphQLTypeAttribute>();
			var requiredAttribute = type.GetCustomAttribute<RequiredAttribute>();
			var typeName = typeAttribute?.TypeName ?? type.Name;
			if (type.IsArray)
			{
				typeName = $"[{typeName}]";
			}
			if (requiredAttribute != null)
			{
				typeName += "!";
			}
			return typeName;
		}
		
		public static object GetValueFromExpression(Expression expr)
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
	}
}