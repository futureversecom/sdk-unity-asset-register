// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Builder
{
	public class QuerySubBuilder<TBuilder, TModel, TInput>
		: MemberSubBuilder<TBuilder, TModel>, IParameterProvider, IInputProvider
		where TBuilder : IBuilder where TModel : IModel where TInput : class, IInput
	{
		public List<IParameter> Parameters { get; } = new();
		public IInput Input { get; }

		public QuerySubBuilder(TBuilder parentBuilder, IQuery<TModel, TInput> query) : base(parentBuilder, "")
		{
			Input = query.Input;

			var queryName = Utils.GetSchemaName<TModel>();
			var inputType = typeof(TInput);
			var fields = inputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
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
				Parameters.Add(new ParameterInfo(parameterName, typeName));
			}

			TokenString = $"{queryName} ({string.Join(", ", Parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"))})";
		}
	}
}