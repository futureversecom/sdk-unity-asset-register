// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Plugins.AssetRegisterV2.Runtime.Schemas;

namespace Plugins.AssetRegisterV2.Runtime.Builder
{
	public class QuerySubBuilder<TBuilder, TModel> : IQuerySubBuilder<TBuilder, TModel>, IQueryAssembler, IToken
		where TBuilder : IBuilder, IQueryAssembler
	{
		private readonly TBuilder _parentBuilder;
		private readonly FieldTreeBuilder _fieldTreeBuilder;
		private Expression _storedExpression;

		public QuerySubBuilder(TBuilder parentBuilder)
		{
			_parentBuilder = parentBuilder;
			_fieldTreeBuilder = new FieldTreeBuilder("asset");
		}

		public TBuilder Done()
		{
			_parentBuilder.RegisterToken(this);
			return _parentBuilder;
		}

		public IQuerySubBuilder<TBuilder, TModel> WithField<TField>(Expression<Func<TModel, TField>> fieldSelector)
		{
			BuildPath(fieldSelector.Body);
			return this;
		}

		public IQuerySubBuilder<TBuilder, TModel> WithFragment(Fragment<TModel> fragment)
		{
			return this;
		}

		public IUnionSubBuilder<IQuerySubBuilder<TBuilder, TModel>, TField> WithUnion<TField>(
			Expression<Func<TModel, TField>> fieldSelector) where TField : IUnion
		{
			_storedExpression = fieldSelector.Body;
			return new UnionSubBuilder<QuerySubBuilder<TBuilder, TModel>, TField>(this);
		}

		public IInterfaceSubBuilder<IQuerySubBuilder<TBuilder, TModel>, TField> WithInterface<TField>(
			Expression<Func<TModel, TField>> fieldSelector) where TField : IInterface
		{
			_storedExpression = fieldSelector.Body;
			return new InterfaceSubBuilder<QuerySubBuilder<TBuilder, TModel>, TField>(this);
		}

		public IRequest Build()
		{
			return Done().Build();
		}

		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await Done().Execute(client, authToken, cancellationToken);
		}

		public void RegisterToken(IToken token)
		{
			var fieldStack = new Stack<string>();
			fieldStack.Push(token.Serialize());
			BuildPath(_storedExpression, fieldStack);
		}

		private void BuildPath(Expression expression, Stack<string> path = null)
		{
			path ??= new Stack<string>();
			while (expression != null && expression.NodeType != ExpressionType.Parameter)
			{
				// TODO: Handle functions
				var memberExpression = (MemberExpression)expression;
				var member = memberExpression.Member;
			
				var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
				if (jsonProperty == null)
				{
					break;
				}

				var name = jsonProperty.PropertyName;
				path.Push(name);
				
				expression = memberExpression.Expression;
			}
			
			_fieldTreeBuilder.WithPath(path);
		}

		public void RegisterParameter(IParameter parameter)
		{
			_parentBuilder.RegisterParameter(parameter);
		}

		public string Serialize()
		{
			return _fieldTreeBuilder.Serialize();
		}
	}
}