// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
#if USING_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif

namespace AssetRegister.Runtime.Builder
{
	internal class MemberSubBuilder<TBuilder, TType> : IMemberSubBuilder<TBuilder, TType>, ITokenProvider
		where TBuilder : IBuilder
	{
		public List<IProvider> Children { get; } = new();
		public string TokenString { get; }
		
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

		public IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> OnMember<TField>(
			Expression<Func<TType, TField>> memberSelector)
		{
			if (memberSelector.Body is not MemberExpression memberExpression)
			{
				throw new ArgumentException(".OnMember() expression must end with a member type");
			}
			
			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = ProcessPath(memberExpression.Expression);
			var builder = new MemberSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> OnArray<TField, TArray>(
			Expression<Func<TType, TArray>> arraySelector)
			where TArray : IEnumerable<TField>
		{
			if (arraySelector.Body is not MemberExpression memberExpression || !memberExpression.Type.IsArray)
			{
				throw new ArgumentException(".OnArray() expression must end with an array type");
			}
			
			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = ProcessPath(memberExpression.Expression);
			var builder = new MemberSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> OnMethod<TField>(Expression<Func<TType, TField>> methodSelector)
		{
			if (methodSelector.Body is not MethodCallExpression methodExpression)
			{
				throw new ArgumentException(".OnMethod() expression must end with a method call");
			}

			var token = ProcessPath(methodExpression.Object);
			var builder =
				MethodSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>.FromMethodCallExpression(
					this,
					methodExpression
				);
			token.Children.Add(builder);
			return builder;
		}

		public IUnionSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> OnUnion<TField>(
			Expression<Func<TType, TField>> unionSelector) where TField : class, IUnion
		{
			if (unionSelector.Body is not MemberExpression memberExpression)
			{
				throw new ArgumentException(".OnUnion() expression must end with a Union type");
			}

			if ((memberExpression.Member is FieldInfo fieldInfo &&
				!typeof(IUnion).IsAssignableFrom(fieldInfo.FieldType)) ||
				(memberExpression.Member is PropertyInfo propertyInfo &&
				!typeof(IUnion).IsAssignableFrom(propertyInfo.PropertyType)))
			{
				throw new ArgumentException(".OnUnion() expression must end with a Union type");
			}

			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = ProcessPath(memberExpression.Expression);
			var builder = new UnionSubBuilder<MemberSubBuilder<TBuilder, TType>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		private IProvider ProcessPath(Expression expression)
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

			ITokenProvider currentProvider = this;
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

		public IRequest Build()
		{
			return _parentBuilder.Build();
		}

#if USING_UNITASK
		public async UniTask<IResponse> Execute(
			IClient client,
			CancellationToken cancellationToken = default)
		{
			return await _parentBuilder.Execute(client, cancellationToken);
		}
#else
		public IEnumerator Execute(IClient client, Action<IResponse> callback)
		{
			return _parentBuilder.Execute(client, callback);
		}
#endif
	}
}