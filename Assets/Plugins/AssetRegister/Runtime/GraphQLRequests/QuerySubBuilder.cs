// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class QuerySubBuilder<TModel, TVariables> : ASubBuilder<TModel, TVariables, QuerySubBuilder<TModel, TVariables>>, IQueryData
		where TModel : class, IModel
		where TVariables : class, IArguments
	{
		private readonly RequestBuilder<IQueryData> _parent;
		
		public QuerySubBuilder(RequestBuilder<IQueryData> parent) : base()
		{
			_parent = parent;
		}

		public RequestBuilder<IQueryData> Submit()
		{
			return _parent.RegisterQuery(this);
		}

		public override GraphQLRequest Build()
		{
			return _parent.Build();
		}
	}
}