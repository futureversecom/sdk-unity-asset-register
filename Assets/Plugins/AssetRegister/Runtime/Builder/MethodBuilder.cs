// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Builder
{
	public class InputDictionary : Dictionary<string, object>, IInput
	{
		
	}
	
	public class MethodSubBuilder<TBuilder, TType>
		: MemberSubBuilder<TBuilder, TType>, IParameterProvider, IInputProvider where TBuilder : IBuilder
	{
		public List<IParameter> Parameters { get; } = new();
		public IInput Input => _inputDictionary;
		
		private readonly InputDictionary _inputDictionary = new();

		public MethodSubBuilder(TBuilder parentBuilder, MethodCallExpression expression) : base(parentBuilder, "")
		{
			var method = expression.Method;
			var parameters = method.GetParameters();
						
			for (var i = 0; i < expression.Arguments.Count; i++)
			{
				var argExpr = expression.Arguments[i];
				var paramInfo = parameters[i];

				var attribute = paramInfo.GetCustomAttribute<GraphQLTypeAttribute>();
				var requiredAttribute = paramInfo.GetCustomAttribute<RequiredAttribute>();
				var parameterTypeName =
					attribute == null ? paramInfo.ParameterType.Name : attribute.TypeName;
				if (paramInfo.ParameterType.IsArray)
				{
					parameterTypeName = $"[{parameterTypeName}]";
				}
				if (requiredAttribute != null)
				{
					parameterTypeName += "!";
				}
				var parameterName = paramInfo.Name;
								
				var value = Utils.GetValueFromExpression(argExpr);
							
				_inputDictionary.Add(parameterName, value);
				var parameter = new ParameterInfo(parameterName, parameterTypeName);
				Parameters.Add(parameter);
			}

			TokenString =
				$"{expression.Method.Name} ({string.Join(", ", Parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"))})";
		}
	}
}