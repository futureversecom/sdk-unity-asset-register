// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Threading;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;

namespace AssetRegister.Runtime.Builder
{
	internal class MutationBuilder: RequestBuilder, IMutationBuilder
	{
		protected override RequestType RequestType => RequestType.Mutation;
		
		public IRequest Build()
			=> BuildRequest();

		public UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default)
			=> throw new System.NotImplementedException();

		public IMemberSubBuilder<IMutationBuilder, TModel> Add<TModel, TInput>(IMutation<TModel, TInput> mutation)
			where TModel : IModel where TInput : class, IInput
		{
			var builder = MethodSubBuilder<MutationBuilder, TModel>.FromMutation(this, mutation);
			Providers.Add(builder);
			return builder;
		}
	}
}