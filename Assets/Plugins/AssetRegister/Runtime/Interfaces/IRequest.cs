// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;

namespace AssetRegister.Runtime.Interfaces
{
	/// <summary>
	/// Represents a GraphQL request that can be sent by a Client
	/// </summary>
	public interface IRequest
	{
		/// <summary>
		/// Headers to be added to the http request
		/// </summary>
		Dictionary<string, string> Headers { get; }
		/// <summary>
		/// Generates the JSON body of the request
		/// </summary>
		/// <returns>Dictionary of headers</returns>
		string Serialize();
		/// <summary>
		/// Use to provide or override any input after the Request has been created
		/// </summary>
		/// <param name="input"></param>
		/// <typeparam name="TInput">Type of input to provide</typeparam>
		void OverrideArguments<TInput>(TInput input) where TInput : class, IInput;
	}
}