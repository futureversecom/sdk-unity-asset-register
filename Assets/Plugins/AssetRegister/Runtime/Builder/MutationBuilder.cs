// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Objects;
using Plugins.AssetRegister.Runtime.Schema.Mutations;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Builder
{
	internal class MutationBuilder: RequestBuilder<IMutationBuilder>, IMutationBuilder
	{
		protected override RequestType RequestType => RequestType.Mutation;
		
		public IMemberSubBuilder<IMutationBuilder, TSchema> Add<TSchema, TInput>(IMutation<TSchema, TInput> mutation)
			where TSchema : ISchema where TInput : class, IInput
		{
			var builder = MethodSubBuilder<MutationBuilder, TSchema>.FromMutation(this, mutation);
			Providers.Add(builder);

			var innerName = BuilderUtils.GetSchemaName<TSchema>();
			var innerBuilder = new MemberSubBuilder<IMutationBuilder, TSchema>(this, innerName);
			builder.Children.Add(innerBuilder);
			
			return innerBuilder;
		}

		public IMemberSubBuilder<IMutationBuilder, Namespace> AddCreateNamespaceMutation(string domain, string suffix)
			=> Add(new CreateNamespaceMutation(domain, suffix));
	}
}