// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using AssetRegister.Runtime.RequestBuilder;

namespace AssetRegister.Runtime.Interfaces
{
	public interface IData
	{
		FieldTreeNode RootNode { get; }
		List<ParameterInfo> Parameters { get; }
		IArgs Args { get; }
	}

	public interface IQueryData : IData
	{
		
	}
	
	public interface IMutationData : IData
	{
		string FunctionName { get; }
	}
}