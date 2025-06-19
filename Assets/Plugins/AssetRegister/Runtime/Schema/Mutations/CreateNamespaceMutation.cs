// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using AssetRegister.Runtime.Schema.Input;
using AssetRegister.Runtime.Schema.Objects;
using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Schema.Mutations
{
	public class CreateNamespaceInputWrapper : IInput
	{
		[Required, JsonProperty("createNamespace_input")] public CreateNamespaceInput Input;
		
		public CreateNamespaceInputWrapper(string domain, string suffix)
		{
			Input = new CreateNamespaceInput(domain, suffix);
		}
	}

	public class CreateNamespaceResult : IResult
	{
		[JsonProperty("createNamespace")] public CreateNamespaceSuccess CreateNamespace;
	}
	
	public class CreateNamespaceMutation : IMutation<Namespace, CreateNamespaceInputWrapper>
	{
		public string FunctionName => "createNamespace";
		public CreateNamespaceInputWrapper Input { get; }

		public CreateNamespaceMutation(string domain, string suffix)
		{
			Input = new CreateNamespaceInputWrapper(domain, suffix);
		}
	}
}