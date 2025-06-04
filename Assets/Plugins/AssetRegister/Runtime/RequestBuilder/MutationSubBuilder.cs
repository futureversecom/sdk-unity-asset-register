// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class MutationSubBuilder<TModel, TVariables> : ASubBuilder<TModel, TVariables, MutationSubBuilder<TModel, TVariables>>, IMutationData
		where TModel : class, IModel
		where TVariables : class, IArguments
	{
		private readonly MutationRequestBuilder _parent;
		
		public MutationSubBuilder(MutationRequestBuilder parent)
		{
			_parent = parent;
		}

		public MutationSubBuilder<TModel, TVariables> WithFunctionName(string functionName)
		{
			FunctionName = functionName;
			return this;
		}

		public MutationRequestBuilder Done()
		{
			return _parent.RegisterQuery(this) as MutationRequestBuilder;
		}

		public Request Build()
		{
			return Done().Build();
		}

		public string FunctionName { get; private set; }
	}
}