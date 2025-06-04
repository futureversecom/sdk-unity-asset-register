// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.RequestBuilder
{
	internal class MutationSubBuilder<TModel, TArgs, TParent> : IMutationSubBuilder<TModel, TArgs, TParent, IMutationData>, IMutationData
		where TModel : class, IModel where TArgs : class, IArgs where TParent : IMainBuilder<TParent, IMutationData>
	{
		public FieldTreeNode RootNode => _rootNode;
		public IArgs Args { get; private set; }
		public string FunctionName { get; private set; }
		public List<ParameterInfo> Parameters { get; }

		private TParent _parent;
		private FieldTreeNode _rootNode;

		public MutationSubBuilder(TParent parent)
		{
			_parent = parent;
			_rootNode = BuilderUtils.RootNodeFromModel<TModel>();
			Parameters = BuilderUtils.ParametersFromType<TArgs>();
		}

		public IRequest Build()
		{
			return Done()
				.Build();
		}

		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await Done()
				.Execute(client, authToken, cancellationToken);
		}

		public TParent Done()
		{
			return _parent.RegisterData(this);
		}

		public IMutationSubBuilder<TModel, TArgs, TParent, IMutationData> WithArgs(TArgs arguments)
		{
			Args = arguments;
			return this;
		}

		public IMutationSubBuilder<TModel, TArgs, TParent, IMutationData> WithField<TField>(
			Expression<Func<TModel, TField>> fieldExpression)
		{
			BuilderUtils.PopulateFieldTree<TModel, TField>(fieldExpression.Body, ref _rootNode);
			return this;
		}
		
		public IMutationSubBuilder<TModel, TArgs, TParent, IMutationData> WithFunctionName(string functionName)
		{
			FunctionName = functionName;
			return this;
		}
	}
}