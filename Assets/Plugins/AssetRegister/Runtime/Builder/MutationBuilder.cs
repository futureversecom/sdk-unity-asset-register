// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	internal class MutationBuilder: RequestBuilder<IMutationBuilder>, IMutationBuilder
	{
		protected override RequestType RequestType => RequestType.Mutation;
		
		public IMemberSubBuilder<IMutationBuilder, TModel> Add<TModel, TInput>(IMutation<TModel, TInput> mutation)
			where TModel : IModel where TInput : class, IInput
		{
			var builder = MethodSubBuilder<MutationBuilder, TModel>.FromMutation(this, mutation);
			Providers.Add(builder);
			return builder;
		}
	}
}