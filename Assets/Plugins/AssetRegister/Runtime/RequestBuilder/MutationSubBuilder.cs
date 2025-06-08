// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AssetRegister.Runtime.Interfaces;
#if USING_UNITASK
using Cysharp.Threading.Tasks;
using System.Threading;
#else
using System.Collections;
#endif

namespace AssetRegister.Runtime.RequestBuilder
{
	internal class MutationSubBuilder<TModel, TInput, TParent> : IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, IMutationData
		where TModel : class, IModel where TInput : class, IInput where TParent : IMainBuilder<TParent, IMutationData>
	{
		public FieldTreeNode RootNode => _rootNode;
		public IInput Input { get; private set; }
		public string FunctionName { get; private set; }
		public List<ParameterInfo> Parameters { get; }

		private TParent _parent;
		private FieldTreeNode _rootNode;

		public MutationSubBuilder(TParent parent)
		{
			_parent = parent;
			_rootNode = BuilderUtils.RootNodeFromModel<TModel>();
			Parameters = BuilderUtils.ParametersFromType<TInput>();
		}

		public IRequest Build()
		{
			return Done()
				.Build();
		}
		
		public TParent Done()
		{
			return _parent.RegisterData(this);
		}

#if USING_UNITASK
		public async UniTask<IResponse> Execute(
			IClient client,
			string authToken = null,
			CancellationToken cancellationToken = default)
		{
			return await Done()
				.Execute(client, authToken, cancellationToken);
		}
#else
		public IEnumerator Execute(
			IClient client,
			string authToken = null,
			Action<IResponse> callback = null)
		{
			return Done().Execute(client, authToken, callback);
		}
#endif
		
		public IMutationSubBuilder<TModel, TInput, TParent, IMutationData> WithInput(TInput arguments)
		{
			Input = arguments;
			return this;
		}

		public IMutationSubBuilder<TModel, TInput, TParent, IMutationData> WithField<TField>(
			Expression<Func<TModel, TField>> fieldExpression)
		{
			BuilderUtils.PopulateFieldTree<TModel, TField>(fieldExpression.Body, ref _rootNode);
			return this;
		}

		public IUnionBuilder<
			UnionBuilder<IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TField, TModel, TInput,
				IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TParent, IMutationData>,
			IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TField, TModel, TInput,
			IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TParent, IMutationData> WithUnionField<TField>(
			Expression<Func<TModel, TField>> fieldExpression) where TField : IUnion
		{
			return new UnionBuilder<IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TField, TModel, TInput,
				IMutationSubBuilder<TModel, TInput, TParent, IMutationData>, TParent, IMutationData>(this);
		}

		public IMutationSubBuilder<TModel, TInput, TParent, IMutationData> WithFunctionName(string functionName)
		{
			FunctionName = functionName;
			return this;
		}
	}
}