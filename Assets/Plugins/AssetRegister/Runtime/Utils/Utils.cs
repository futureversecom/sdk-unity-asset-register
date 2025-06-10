// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Utils
{
	internal static class Utils
	{
		public static string GetSchemaName<TSchema>() where TSchema : ISchema
		{
			var type = typeof(TSchema);
			var attribute = type.GetCustomAttribute<JsonObjectAttribute>();
			return attribute == null ? type.Name : attribute.Id;
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