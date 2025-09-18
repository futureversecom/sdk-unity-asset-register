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

		private IMemberSubBuilder<IQueryBuilder, TSchema> AddMethodBuilder<TSchema, TInput>(IQuery<TSchema, TInput> query)
			where TSchema : ISchema where TInput : class, IInput
		{
			var builder = MethodSubBuilder<QueryBuilder, TSchema>.FromQuery(this, query);
			Providers.Add(builder);
			return builder;
		}
        
        private IInterfaceSubBuilder<IQueryBuilder, TSchema> AddInterfaceBuilder<TSchema, TInput>(IQuery<TSchema, TInput> query)
            where TSchema : ISchema, IInterface where TInput : class, IInput
        {
            var builder = MethodSubBuilder<QueryBuilder, TSchema>.FromQuery(this, query);
            var interfaceBuilder = new InterfaceSubBuilder<QueryBuilder, TSchema>(this, null);
            builder.Children.Add(interfaceBuilder);
            Providers.Add(builder);
            return interfaceBuilder;
        }

		public IMemberSubBuilder<IQueryBuilder, Account> AddAccountsQuery(string[] addresses)
			=> AddMethodBuilder(new AccountsQuery(addresses));

		public IMemberSubBuilder<IQueryBuilder, Namespace> AddNamespaceQuery(string @namespace)
			=> AddMethodBuilder(new NamespaceQuery(@namespace));

        public IMemberSubBuilder<IQueryBuilder, AssetImagesConnection> AddAssetImagesQuery(
            string collectionId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new AssetImagesQuery(collectionId, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, Asset> AddAssetQuery(string collectionId, string tokenId)
            => AddMethodBuilder(new AssetQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, Asset> AddAssetsByIdsQuery(AssetInput[] assetIds)
            => AddMethodBuilder(new AssetsByIdsQuery(assetIds));

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
            => AddMethodBuilder(new AssetsQuery(
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
            => AddMethodBuilder(new CollectionsBySchemaQuery(schemaId));

        public IMemberSubBuilder<IQueryBuilder, CollectionConnection> AddCollectionsQuery(
            string[] addresses,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new CollectionsQuery(addresses, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, DomainsConnection> AddDomainsQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new DomainsQuery(before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, GenericTokenBalance> AddGenericTokenBalancesQuery(
            GenericTokenFilter filter = default,
            string[] addresses = null,
            string[] genericTokenIds = null)
            => AddMethodBuilder(new GenericTokenBalancesQuery(addresses, filter, genericTokenIds));

        public IMemberSubBuilder<IQueryBuilder, NoSchema> AddGetNonceForChainAddressQuery(NonceInput input)
            => AddMethodBuilder(new GetNonceForChainAddressQuery(input));

        public IMemberSubBuilder<IQueryBuilder, NamespacesConnection> AddNamespacesQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new NamespacesQuery(before, after, first, last));

        public IInterfaceSubBuilder<IQueryBuilder, INode> AddNodeQuery(string id)
            => AddInterfaceBuilder(new NodeQuery(id));

        public IMemberSubBuilder<IQueryBuilder, OffChainAsset> AddOffChainAssetQuery(
            string collectionId,
            string tokenId = default)
            => AddMethodBuilder(new OffChainAssetQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, OffChainAssetsConnection> AddOffChainAssetsQuery(
            OffChainAssetsInput input,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new OffChainAssetsQuery(input, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, AssetOwnersConnection> AddOwnersQuery(
            string[] collectionIds,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new OwnersQuery(collectionIds, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, SchemaCustomDomain> AddSchemaCustomDomainQuery(string domainName)
            => AddMethodBuilder(new SchemaCustomDomainQuery(domainName));

        public IMemberSubBuilder<IQueryBuilder, TokenSchema> AddTokenSchemaQuery(
            string collectionId,
            string tokenId = default)
            => AddMethodBuilder(new TokenSchemaQuery(collectionId, tokenId));

        public IMemberSubBuilder<IQueryBuilder, TokenSchemasConnection> AddTokenSchemasQuery(
            string collectionId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new TokenSchemasQuery(collectionId, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, Transaction> AddTransactionQuery(string transactionHash)
            => AddMethodBuilder(new TransactionQuery(transactionHash));

        public IMemberSubBuilder<IQueryBuilder, TransactionsConnection> AddTransactionsQuery(
            string address,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new TransactionsQuery(address, before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, WebhookEndpoint> AddWebhookEndpointQuery(string webhookId)
            => AddMethodBuilder(new WebhookEndpointQuery(webhookId));

        public IMemberSubBuilder<IQueryBuilder, WebhookEndpointsConnection> AddWebhookEndpointsQuery(
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new WebhookEndpointsQuery(before, after, first, last));

        public IMemberSubBuilder<IQueryBuilder, WebhookSubscription> AddWebhookSubscriptionQuery(string subscriptionId)
            => AddMethodBuilder(new WebhookSubscriptionQuery(subscriptionId));

        public IMemberSubBuilder<IQueryBuilder, WebhookSubscriptionsConnection> AddWebhookSubscriptionsQuery(
            string webhookId,
            string before = default,
            string after = default,
            float first = default,
            float last = default)
            => AddMethodBuilder(new WebhookSubscriptionsQuery(webhookId, before, after, first, last));

	}
}