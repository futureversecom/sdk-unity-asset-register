// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

namespace AssetRegister.Runtime.Interfaces
{
	public interface IQueryAssembler
	{
		void RegisterToken(IToken token);
		void RegisterParameter(IParameter parameter);
		void RegisterInput(object input);
	}
}