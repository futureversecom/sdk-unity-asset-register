// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class MutationSubBuilder<TModel, TVariables> : ASubBuilder<TModel, TVariables, MutationSubBuilder<TModel, TVariables>>, IMutationData
		where TModel : class, IModel
		where TVariables : class, IArguments
	{
		private readonly RequestBuilder<IMutationData> _parent;
		
		public MutationSubBuilder(RequestBuilder<IMutationData> parent) : base()
		{
			_parent = parent;
		}

		public RequestBuilder<IMutationData> Submit()
		{
			return _parent.RegisterQuery(this);
		}

		public override GraphQLRequest Build()
		{
			return _parent.Build();
		}

		public string FunctionName { get; }
	}
}