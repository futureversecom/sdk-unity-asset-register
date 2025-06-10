// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	internal class QueryBuilder : RequestBuilder, IQueryBuilder
	{
		protected override RequestType RequestType => RequestType.Query;
		
		public IRequest Build()
			=> BuildRequest();

		public UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default)
			=> throw new System.NotImplementedException();

		public IMemberSubBuilder<IQueryBuilder, TModel> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : IModel where TInput : class, IInput
		{
			var builder = MethodSubBuilder<QueryBuilder, TModel>.FromQuery(this, query);
			Providers.Add(builder);
			return builder;
		}
	}
}