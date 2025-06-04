// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class QuerySubBuilder<TModel, TArgs, TParent> : IQuerySubBuilder<TModel, TArgs, TParent, IQueryData>, IQueryData
		where TModel : class, IModel
		where TArgs : class, IArguments
		where TParent : class, IMainBuilder<TParent, IQueryData>
	{
		public FieldTreeNode RootNode => _rootNode;
		public List<ParameterInfo> Parameters { get; }
		public IArguments Args { get; private set; }
		
		private readonly TParent _parent;
		private FieldTreeNode _rootNode;

		public QuerySubBuilder(TParent parent)
		{
			_parent = parent;
			_rootNode = BuilderUtils.RootNodeFromModel<TModel>();
			Parameters = BuilderUtils.ParametersFromType<TArgs>();
		}

		public Request Build()
		{
			return _parent.Build();
		}

		public async UniTask<Result> Execute(IAssetRegisterClient client, string authToken, CancellationToken cancellationToken)
		{
			return await _parent.Execute(client, authToken, cancellationToken);
		}
		
		public TParent Done()
		{
			return _parent.RegisterData(this);
		}

		public IQuerySubBuilder<TModel, TArgs, TParent, IQueryData> WithArgs(TArgs arguments)
		{
			Args = arguments;
			return this;
		}

		public IQuerySubBuilder<TModel, TArgs, TParent, IQueryData> WithField<TField>(
			Expression<Func<TModel, TField>> fieldExpression)
		{
			BuilderUtils.PopulateFieldTree<TModel, TField>(fieldExpression.Body, ref _rootNode);
			return this;
		}
	}
}