// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Core;
using AssetRegister.Runtime.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace AssetRegister.Runtime.Builder
{
	public class ParameterInfo : IParameter
	{
		public string ParameterName { get; }
		public string ParameterType { get; }

		public ParameterInfo(string parameterName, string parameterType)
		{
			ParameterName = parameterName;
			ParameterType = parameterType;
		}
	}
	
	public class QueryBuilder : IQueryBuilder, IQueryAssembler
	{
		private readonly List<IToken> _tokens = new();
		private readonly List<IParameter> _parameters = new();
		private readonly List<IInput> _inputs = new();

		public IRequest Build()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("query (");
			stringBuilder.Append(string.Join(", ", _parameters.Select(p => $"${p.ParameterName}: {p.ParameterType}")));
			stringBuilder.AppendLine(") {");
			foreach (var token in _tokens)
			{
				stringBuilder.Append(token.Serialize());
			}
			stringBuilder.Append("}");
			var queryString = stringBuilder.ToString();
			Debug.Log(queryString);
			
			var inputObject = new JObject();
			foreach (var input in _inputs)
			{
				inputObject.Merge(JObject.FromObject(input));
			}
			
			return new Request(queryString, inputObject);
		}

		public UniTask<IResponse> Execute(IClient client, string authToken = null, CancellationToken cancellationToken = default)
			=> throw new System.NotImplementedException();

		public IQuerySubBuilder<IQueryBuilder, TModel> Add<TModel, TInput>(IQuery<TModel, TInput> query)
			where TModel : class, IModel where TInput : class, IInput
		{
			RegisterInput(query.Input);
			
			var inputType = typeof(TInput);
			var fields = inputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
			var parameters = new List<IParameter>();
			foreach (var field in fields)
			{
				var typeAttribute = field.GetCustomAttribute<GraphQLTypeAttribute>();
				var requiredAttribute = field.GetCustomAttribute<RequiredAttribute>();
				var jsonAttribute = field.GetCustomAttribute<JsonPropertyAttribute>();
				
				var typeName = typeAttribute?.TypeName ?? field.FieldType.Name;
				if (field.FieldType.IsArray)
				{
					typeName = $"[{typeName}]";
				}
				if (requiredAttribute != null)
				{
					typeName += "!";
				}
				
				var parameterName = jsonAttribute?.PropertyName ?? field.Name;
				var parameter = new ParameterInfo(parameterName, typeName);
				parameters.Add(parameter);
				RegisterParameter(parameter);
			}
			
			return new QuerySubBuilder<QueryBuilder, TModel>(this, parameters);
		}

		public void RegisterToken(IToken token)
		{
			_tokens.Add(token);
		}

		public void RegisterParameter(IParameter parameter)
		{
			_parameters.Add(parameter);
		}

		public void RegisterInput(IInput input)
		{
			_inputs.Add(input);
		}
	}
}