// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using AssetRegister.Runtime.Interfaces;
#if USING_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else

#endif

namespace AssetRegister.Runtime.RequestBuilder
{
	internal class QuerySubBuilder<TModel, TInput, TParent> : IQuerySubBuilder<TModel, TInput, TParent, IQueryData>, IQueryData
		where TModel : class, IModel
		where TInput : class, IInput
		where TParent : class, IMainBuilder<TParent, IQueryData>
	{
		public FieldTreeNode RootNode => _rootNode;
		public List<ParameterInfo> Parameters { get; }
		public IInput Input { get; private set; }
		
		private readonly TParent _parent;
		private FieldTreeNode _rootNode;

		public QuerySubBuilder(TParent parent)
		{
			_parent = parent;
			_rootNode = BuilderUtils.RootNodeFromModel<TModel>();
			Parameters = BuilderUtils.ParametersFromType<TInput>();
		}

		public IRequest Build()
		{
			return _parent.Build();
		}

#if USING_UNITASK
		public async UniTask<IResponse> Execute(IClient client, string authToken, CancellationToken cancellationToken)
		{
			return await Done().Execute(client, authToken, cancellationToken);
		}
#else
		public IEnumerator Execute(IClient client, string authToken = null, Action<IResponse> callback = null)
		{
			return Done().Execute(client, authToken, callback);
		}
#endif
		
		public TParent Done()
		{
			return _parent.RegisterData(this);
		}

		public IQuerySubBuilder<TModel, TInput, TParent, IQueryData> WithInput(TInput arguments)
		{
			Input = arguments;
			return this;
		}

		public IQuerySubBuilder<TModel, TInput, TParent, IQueryData> WithField<TField>(
			Expression<Func<TModel, TField>> fieldExpression)
		{
			BuilderUtils.PopulateFieldTree<TModel, TField>(fieldExpression.Body, ref _rootNode);
			return this;
		}
	}
}