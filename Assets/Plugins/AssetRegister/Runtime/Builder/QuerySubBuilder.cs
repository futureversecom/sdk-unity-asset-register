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
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Builder
{
	public class QuerySubBuilder<TBuilder, TSchema> : IQuerySubBuilder<TBuilder, TSchema>, IQueryAssembler, IToken
		where TBuilder : IBuilder, IQueryAssembler
		where TSchema : class, ISchema
	{
		private readonly TBuilder _parentBuilder;
		private readonly FieldTreeBuilder _fieldTreeBuilder;
		private Expression _storedExpression;

		public QuerySubBuilder(TBuilder parentBuilder, List<IParameter> parameters = null)
		{
			_parentBuilder = parentBuilder;
			
			var queryName = Utils.GetSchemaName<TSchema>();
			var queryParameters = parameters == null || parameters.Count == 0 ?
				null :
				string.Join(", ", parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"));
			var queryString = $"{queryName} ({queryParameters})";
			
			_fieldTreeBuilder = new FieldTreeBuilder(queryString);
		}

		public TBuilder Done()
		{
			_parentBuilder.RegisterToken(this);
			return _parentBuilder;
		}

		public IQuerySubBuilder<TBuilder, TSchema> WithField<TField>(Expression<Func<TSchema, TField>> fieldSelector)
		{
			BuildPath(fieldSelector.Body);
			return this;
		}

		public IQuerySubBuilder<TBuilder, TSchema> WithFragment(Fragment<TSchema> fragment)
		{
			return this;
		}

		public IUnionSubBuilder<IQuerySubBuilder<TBuilder, TSchema>, TField> WithUnion<TField>(
			Expression<Func<TSchema, TField>> fieldSelector) where TField : class, IUnion
		{
			_storedExpression = fieldSelector.Body;
			return new UnionSubBuilder<QuerySubBuilder<TBuilder, TSchema>, TField>(this);
		}

		public IInterfaceSubBuilder<IQuerySubBuilder<TBuilder, TSchema>, TField> WithInterface<TField>(
			Expression<Func<TSchema, TField>> fieldSelector) where TField : IInterface
		{
			_storedExpression = fieldSelector.Body;
			return new InterfaceSubBuilder<QuerySubBuilder<TBuilder, TSchema>, TField>(this);
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

		public void RegisterInput(IInput input)
		{
			_parentBuilder.RegisterInput(input);
		}

		public string Serialize()
		{
			return _fieldTreeBuilder.Serialize();
		}
	}
}