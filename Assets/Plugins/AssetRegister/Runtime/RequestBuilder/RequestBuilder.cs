// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace Plugins.AssetRegister.Runtime.Requests
{
	public static class RequestBuilder
	{
		public static IQueryBuilder Query()
			=> new QueryBuilder();

		public static IMutationBuilder Mutation()
			=> new MutationBuilder();
	}
}