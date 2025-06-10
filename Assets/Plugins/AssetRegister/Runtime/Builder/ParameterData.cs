// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using AssetRegister.Runtime.Interfaces;

namespace AssetRegister.Runtime.Builder
{
	internal class ParameterData : IParameter
	{
		public string ParameterName { get; }
		public string ParameterType { get; }

		public ParameterData(string parameterName, string parameterType)
		{
			ParameterName = parameterName;
			ParameterType = parameterType;
		}
	}
}