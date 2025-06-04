// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.RequestBuilder
{
	public static class RequestBuilder
	{
		public static IQueryBuilder BeginQuery()
			=> new QueryBuilder();

		public static IMutationBuilder BeginMutation()
			=> new MutationBuilder();
	}
}