// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;

namespace AssetRegister.Runtime.Builder
{
	internal class QueryBuilder : RequestBuilder<IQueryBuilder>, IQueryBuilder
	{
		protected override RequestType RequestType => RequestType.Query;

		private IMemberSubBuilder<IQueryBuilder, TSchema> Add<TSchema, TInput>(IQuery<TSchema, TInput> query)
			where TSchema : ISchema where TInput : class, IInput
		{
			var builder = MethodSubBuilder<QueryBuilder, TSchema>.FromQuery(this, query);
			Providers.Add(builder);
			return builder;
		}

		public IMemberSubBuilder<IQueryBuilder, Account> AddAccountsQuery(string[] addresses)
			=> Add(new AccountsQuery(addresses));

		public IMemberSubBuilder<IQueryBuilder, AssetImagesConnection> AddAssetImagesQuery(
			string collectionId,
			string before = default,
			string after = default,
			float first = default,
			float last = default)
			=> Add(new AssetImagesQuery(collectionId, before, after, first, last));

		public IMemberSubBuilder<IQueryBuilder, Asset> AddAssetQuery(string collectionId, string tokenId)
			=> Add(new AssetQuery(collectionId, tokenId));

		public IMemberSubBuilder<IQueryBuilder, Namespace> AddNamespaceQuery(string @namespace)
			=> Add(new NamespaceQuery(@namespace));
	}
}