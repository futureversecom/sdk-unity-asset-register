// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace AssetRegister.Runtime.Builder
{
	public class MemberSubBuilder<TBuilder, TType> : IMemberSubBuilder<TBuilder, TType>, ITokenProvider
		where TBuilder : IBuilder
	{
		public List<IProvider> Children { get; } = new();
		public string TokenString { get; protected set; }
		
		private readonly TBuilder _parentBuilder;

		public MemberSubBuilder(TBuilder parentBuilder, string memberName)
		{
			_parentBuilder = parentBuilder;
			TokenString = memberName;
		}

		public TBuilder Done()
			=> _parentBuilder;

		public IMemberSubBuilder<TBuilder, TType> WithField<TField>(Expression<Func<TType, TField>> fieldSelector)
		{
			ProcessPath(fieldSelector.Body);
			return this;
		}

		public IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithMethod<TField>(Expression<Func<TType, TField>> fieldSelector)
		{
			if (fieldSelector.Body is not MethodCallExpression methodExpression)
			{
				throw new ArgumentException(".WithMethod() expression must end with a method call");
			}

			var token = ProcessPath(methodExpression.Object);
			var builder = new MethodSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>(this, methodExpression);
			token.Children.Add(builder);
			return builder;
		}

		public IUnionSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithUnion<TField>(
			Expression<Func<TType, TField>> fieldSelector) where TField : class, IUnion
		{
			if (fieldSelector.Body is not MemberExpression memberExpression)
			{
				throw new ArgumentException(".WithUnion() expression must end with a Union type");
			}

			if ((memberExpression.Member is FieldInfo fieldInfo &&
				!typeof(IUnion).IsAssignableFrom(fieldInfo.FieldType)) ||
				(memberExpression.Member is PropertyInfo propertyInfo &&
				!typeof(IUnion).IsAssignableFrom(propertyInfo.PropertyType)))
			{
				throw new ArgumentException(".WithUnion() expression must end with a Union type");
			}

			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = ProcessPath(memberExpression.Expression);
			var builder = new UnionSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IInterfaceSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithInterface<TField>(
			Expression<Func<TType, TField>> fieldSelector) where TField : IInterface
		{
			throw new NotImplementedException("Interfaces are currently unsupported");
		}

		private IProvider ProcessPath(Expression expression)
		{
			var stack = new Stack<string>();
			while (expression != null && expression.NodeType != ExpressionType.Parameter)
			{
				if (expression is not MemberExpression memberExpression)
				{
					throw new ArgumentException("Used .WithField() with non-member expression. Use WithMethod instead");
				}
				
				var member = memberExpression.Member; 
				
				// if (typeof(IUnion).IsAssignableFrom(member.DeclaringType))
				// {
				// 	throw new ArgumentException("Used .WithField() with Union member. Use WithUnion instead");
				// }
				
				var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
				var name = jsonProperty?.PropertyName ?? member.Name;
				stack.Push(name);
				
				expression = memberExpression.Expression;
			}

			ITokenProvider currentProvider = this;
			while (stack.TryPop(out var name))
			{
				var existingBuilder =
					currentProvider.Children.FirstOrDefault(c => c is ITokenProvider t && t.TokenString == name);
				if (existingBuilder == null)
				{
					existingBuilder = new FieldBuilder(name);
					currentProvider.Children.Add(existingBuilder);
				}

				currentProvider = (ITokenProvider)existingBuilder;
			}

			return currentProvider;
		}

		public IRequest Build()
		{
			return _parentBuilder.Build();
		}

		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await _parentBuilder.Execute(client, authToken, cancellationToken);
		}

		// private void BuildPath(Expression expression, Stack<string> path = null)
		// {
		// 	path ??= new Stack<string>();
		// 	while (expression != null && expression.NodeType != ExpressionType.Parameter)
		// 	{
		// 		switch (expression)
		// 		{
		// 			case MemberExpression memberExpression:
		// 				var member = memberExpression.Member;
		//
		// 				var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
		// 				if (jsonProperty == null)
		// 				{
		// 					break;
		// 				}
		//
		// 				var name = jsonProperty.PropertyName;
		// 				path.Push(name);
		//
		// 				expression = memberExpression.Expression;
		// 				break;
		// 			case MethodCallExpression methodCallExpression:
		// 				var method = methodCallExpression.Method;
		// 				var parameters = method.GetParameters();
		// 				var inputObject = new Dictionary<string, object>();
		// 				var allParams = new List<ParameterInfo>();
		// 				
		// 				for (var i = 0; i < methodCallExpression.Arguments.Count; i++)
		// 				{
		// 					var argExpr = methodCallExpression.Arguments[i];
		// 					var paramInfo = parameters[i];
		//
		// 					var attribute = paramInfo.GetCustomAttribute<GraphQLTypeAttribute>();
		// 					var requiredAttribute = paramInfo.GetCustomAttribute<RequiredAttribute>();
		// 					var parameterTypeName =
		// 						attribute == null ? paramInfo.ParameterType.Name : attribute.TypeName;
		// 					if (paramInfo.ParameterType.IsArray)
		// 					{
		// 						parameterTypeName = $"[{parameterTypeName}]";
		// 					}
		// 					if (requiredAttribute != null)
		// 					{
		// 						parameterTypeName += "!";
		// 					}
		// 					var parameterName = paramInfo.Name;
		// 						
		// 					var value = Utils.GetValueFromExpression(argExpr);
		// 					
		// 					inputObject.Add(parameterName, value);
		// 					var parameter = new ParameterInfo(parameterName, parameterTypeName);
		// 					allParams.Add(parameter);
		// 					//RegisterParameter(parameter);
		// 				}
		// 				
		// 				//RegisterInput(inputObject);
		// 				var parameterString = string.Join(
		// 					", ",
		// 					allParams.Select(p => $"{p.ParameterName}: ${p.ParameterName}")
		// 				);
		// 				path.Push($"{method.Name} ({parameterString})");
		// 				expression = methodCallExpression.Object;
		// 				break;
		// 		}
		// 	}
		// 	
		// 	_fieldBuilder.WithPath(path);
		// }
	}
}