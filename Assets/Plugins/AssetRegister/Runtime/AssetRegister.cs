// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Builder;
using AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime
{
	public static class AssetRegister
	{
		public static IQueryBuilder NewQuery()
		{
			return new QueryBuilder();
		}
		
		public static IMutationBuilder NewMutation()
		{
			return new MutationBuilder();
		}
	}
}