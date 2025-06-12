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

		public IMemberSubBuilder<IMemberSubBuilder<TBuilder, TType>, TField> WithMethod<TField>(Expression<Func<TType, TField>> fieldSelector)
		{
			if (fieldSelector.Body is not MethodCallExpression methodExpression)
			{
				throw new ArgumentException(".WithMethod() expression must end with a method call");
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