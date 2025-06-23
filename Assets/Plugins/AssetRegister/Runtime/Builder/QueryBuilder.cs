// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using AssetRegister.Runtime.Schema.Queries;
using Plugins.AssetRegister.Runtime.Schema.Interfaces;

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

		public IMemberSubBuilder<IQueryBuilder, Namespace> AddNamespaceQuery(string @namespace)
			=> Add(new NamespaceQuery(@namespace));

        public IMemberSubBuilder<IQueryBuilder, AssetImagesConnection> AddAssetImagesQuery(
            string collectionId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new AssetImagesQuery(collectionId, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, Asset> AddAssetQuery(string collectionId, string tokenId)
            => Add(new AssetQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, Asset> AddAssetsByIdsQuery(AssetInput[] assetIds)
            => Add(new AssetsByIdsQuery(assetIds));

        public IMemberSubBuilder<IQueryBuilder, AssetConnection> AddAssetsQuery(
            bool removeDuplicates = default,
            Sort[] sort = default,
            AssetFilter filter = default,
            string schemaId = default,
            string[] collectionIds = default,
            string[] addresses = default,
            string before = default,
            string after = default,
            float first = default,
            float last = default,
            string chainId = default,
            string chainType = default)
            => Add(new AssetsQuery(
                removeDuplicates,
                sort,
                filter,
                schemaId,
                collectionIds,
                addresses,
                before,
                after,
                first,
                last,
                chainId,
                chainType));

        public IMemberSubBuilder<IQueryBuilder, Collection> AddCollectionsBySchemaQuery(string schemaId)
            => Add(new CollectionsBySchemaQuery(schemaId));

        public IMemberSubBuilder<IQueryBuilder, CollectionConnection> AddCollectionsQuery(
            string[] addresses,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new CollectionsQuery(addresses, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, DomainsConnection> AddDomainsQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new DomainsQuery(before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, GenericTokenBalance> AddGenericTokenBalancesQuery(
            GenericTokenFilter filter = default,
            string[] addresses = null,
            string[] genericTokenIds = null)
            => Add(new GenericTokenBalancesQuery(addresses, filter, genericTokenIds));

        public IMemberSubBuilder<IQueryBuilder, NoSchema> AddGetNonceForChainAddressQuery(NonceInput input)
            => Add(new GetNonceForChainAddressQuery(input));

        public IMemberSubBuilder<IQueryBuilder, NamespacesConnection> AddNamespacesQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new NamespacesQuery(before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, INode> AddNodeQuery(string id)
            => Add(new NodeQuery(id));

        public IMemberSubBuilder<IQueryBuilder, OffChainAsset> AddOffChainAssetQuery(
            string collectionId,
            string tokenId = default)
            => Add(new OffChainAssetQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, OffChainAssetsConnection> AddOffChainAssetsQuery(
            OffChainAssetsInput input,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new OffChainAssetsQuery(input, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, AssetOwnersConnection> AddOwnersQuery(
            string[] collectionIds,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new OwnersQuery(collectionIds, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, SchemaCustomDomain> AddSchemaCustomDomainQuery(string domainName)
            => Add(new SchemaCustomDomainQuery(domainName));

        public IMemberSubBuilder<IQueryBuilder, TokenSchema> AddTokenSchemaQuery(
            string collectionId,
            string tokenId = default)
            => Add(new TokenSchemaQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, TokenSchemasConnection> AddTokenSchemasQuery(
            string collectionId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new TokenSchemasQuery(collectionId, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, Transaction> AddTransactionQuery(string transactionHash)
            => Add(new TransactionQuery(transactionHash));

        public IMemberSubBuilder<IQueryBuilder, TransactionsConnection> AddTransactionsQuery(
            string address,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new TransactionsQuery(address, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, WebhookEndpoint> AddWebhookEndpointQuery(string webhookId)
            => Add(new WebhookEndpointQuery(webhookId));

        public IMemberSubBuilder<IQueryBuilder, WebhookEndpointsConnection> AddWebhookEndpointsQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new WebhookEndpointsQuery(before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, WebhookSubscription> AddWebhookSubscriptionQuery(string subscriptionId)
            => Add(new WebhookSubscriptionQuery(subscriptionId));

        public IMemberSubBuilder<IQueryBuilder, WebhookSubscriptionsConnection> AddWebhookSubscriptionsQuery(
            string webhookId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => Add(new WebhookSubscriptionsQuery(webhookId, before, after, first, last));

	}
}