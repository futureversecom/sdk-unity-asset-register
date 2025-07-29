// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Utils;
#if USING_UNITASK && !AR_SDK_NO_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else
using System.Collections;
#endif

namespace AssetRegister.Runtime.Builder
{
	internal class InterfaceSubBuilder<TBuilder, TInterface> : IInterfaceSubBuilder<TBuilder, TInterface>, ITokenProvider
		where TBuilder : IBuilder where TInterface : IInterface
	{
		public string TokenString { get; }
		public List<IProvider> Children { get; } = new();
		
		private readonly TBuilder _parentBuilder;
		
		public InterfaceSubBuilder(TBuilder parentBuilder, string memberName)
		{
			TokenString = memberName;
			_parentBuilder = parentBuilder;
			
			// Typename required for deserialization
			Children.Add(new FieldToken("__typename"));
		}

		public IInterfaceSubBuilder<TBuilder, TInterface> WithField<TField>(Expression<Func<TInterface, TField>> fieldSelector)
		{
			BuilderUtils.ProcessPath(fieldSelector.Body, this);
			return this;
		}

		public IMemberSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TField> OnMember<TField>(
			Expression<Func<TInterface, TField>> memberSelector)
		{
			if (memberSelector.Body is not MemberExpression memberExpression)
			{
				throw new ArgumentException(".OnMember() expression must end with a member type");
			}
			
			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = BuilderUtils.ProcessPath(memberExpression.Expression, this);
			var builder = new MemberSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}
 
		public IMemberSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TField> OnArray<TField, TArray>(
			Expression<Func<TInterface, TArray>> arraySelector)
			where TArray : IEnumerable<TField>
		{
			if (arraySelector.Body is not MemberExpression memberExpression || !BuilderUtils.IsMemberArray(memberExpression))
			{
				throw new ArgumentException(".OnArray() expression must end with an array type");
			}
			
			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = BuilderUtils.ProcessPath(memberExpression.Expression, this);
			var builder = new MemberSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IMemberSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TField> OnMethod<TField>(Expression<Func<TInterface, TField>> methodSelector)
		{
			if (methodSelector.Body is not MethodCallExpression methodExpression)
			{
				throw new ArgumentException(".OnMethod() expression must end with a method call");
			}

			var token = BuilderUtils.ProcessPath(methodExpression.Object, this);
			var builder =
				MethodSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TField>.FromMethodCallExpression(
					this,
					methodExpression
				);
			token.Children.Add(builder);
			return builder;
		}

		public IUnionSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TField> OnUnion<TField>(
			Expression<Func<TInterface, TField>> unionSelector) where TField : class, IUnion
		{
			if (unionSelector.Body is not MemberExpression memberExpression || !BuilderUtils.IsMemberUnion(memberExpression))
			{
				throw new ArgumentException(".OnUnion() expression must end with a Union type");
			}

			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = BuilderUtils.ProcessPath(memberExpression.Expression, this);
			var builder = new UnionSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IInterfaceSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TField> OnInterface<TField>(
			Expression<Func<TInterface, TField>> interfaceSelector) where TField : IInterface
		{
			if (interfaceSelector.Body is not MemberExpression memberExpression || !BuilderUtils.IsMemberInterface(memberExpression))
			{
				throw new ArgumentException(".OnInterface() expression must end with an interface type");
			}

			var attribute = memberExpression.Member.GetCustomAttribute<JsonPropertyAttribute>();
			var name = attribute?.PropertyName ?? memberExpression.Member.Name;

			var token = BuilderUtils.ProcessPath(memberExpression.Expression, this);
			var builder = new InterfaceSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TField>(this, name);
			token.Children.Add(builder);
			return builder;
		}

		public IMemberSubBuilder<IInterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType> On<TInterfaceType>()
			where TInterfaceType : TInterface
		{
			var memberString = $"... on {typeof(TInterfaceType).Name}";
			var builder = new MemberSubBuilder<InterfaceSubBuilder<TBuilder, TInterface>, TInterfaceType>(this, memberString);
			Children.Add(builder);
			return builder;
		}

		public TBuilder Done()
			=> _parentBuilder;

		public IRequest Build()
			=> _parentBuilder.Build();

#if USING_UNITASK && !AR_SDK_NO_UNITASK
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