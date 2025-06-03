// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public interface IQueryData
	{
		FieldTreeNode RootNode { get; }
		List<ParameterInfo> Parameters { get; }
		IArguments Args { get; }
	}
	
	public interface IMutationData : IQueryData
	{
		string FunctionName { get; }
	}
}