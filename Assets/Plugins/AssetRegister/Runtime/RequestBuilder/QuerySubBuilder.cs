// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public class QuerySubBuilder<TModel, TVariables> : ASubBuilder<TModel, TVariables, QuerySubBuilder<TModel, TVariables>>, IQueryData
		where TModel : class, IModel
		where TVariables : class, IArguments
	{
		private readonly QueryRequestBuilder _parent;
		
		public QuerySubBuilder(QueryRequestBuilder parent)
		{
			_parent = parent;
		}

		public QueryRequestBuilder Done()
		{
			return _parent.RegisterQuery(this) as QueryRequestBuilder;
		}

		public Request Build()
		{
			return Done().Build();
		}
	}
}