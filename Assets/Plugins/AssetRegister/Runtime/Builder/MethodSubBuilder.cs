// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AssetRegister.Runtime.Attributes;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Plugins.AssetRegister.Runtime.Utils;

namespace AssetRegister.Runtime.Builder
{
    internal class MethodSubBuilder<TBuilder, TType>
        : MemberSubBuilder<TBuilder, TType>, IParameterProvider, IInputProvider where TBuilder : IBuilder
    {
        public List<IParameter> Parameters { get; }
        public IInput Input { get; }

        private MethodSubBuilder(
            TBuilder parentBuilder,
            string token,
            List<IParameter> parameters,
            IInput input) : base(parentBuilder, token)
        {
            Parameters = parameters;
            Input = input;
        }

        public static MethodSubBuilder<TBuilder, TType> FromMethodCallExpression(
            TBuilder parentBuilder,
            MethodCallExpression expression)
        {
            var method = expression.Method;
            var parameters = method.GetParameters();
            var input = new InputDictionary();
            var parameterList = new List<IParameter>();

            for (var i = 0; i < expression.Arguments.Count; i++)
            {
                var value = Utils.GetValueFromExpression(expression.Arguments[i]);
                
                // Ignore parameter if the value is default
                if (value == null || value.Equals(Utils.GetDefaultValue(value.GetType())))
                {
                    continue;
                }
                
                var paramInfo = parameters[i];
                var parameter = CreateParameterFromInfo(paramInfo);

                input.Add(parameter.ParameterName, value);
                parameterList.Add(parameter);
            }

            var token = BuildTokenString(method.Name, parameterList);
            return new MethodSubBuilder<TBuilder, TType>(
                parentBuilder,
                token,
                parameterList,
                input
            );
        }

        public static MethodSubBuilder<TBuilder, TSchema> FromQuery<TSchema, TInput>(
            TBuilder parentBuilder,
            IQuery<TSchema, TInput> query) where TSchema : ISchema where TInput : class, IInput
        {
            var parameterList = CreateParametersFromInput<TInput>();
            var mutationName = Utils.GetSchemaName<TSchema>();
            var token = BuildTokenString(mutationName, parameterList);
            
            return new MethodSubBuilder<TBuilder, TSchema>(
                parentBuilder,
                token,
                parameterList,
                query.Input
            );
        }
        
        public static MethodSubBuilder<TBuilder, TSchema> FromMutation<TSchema, TInput>(
            TBuilder parentBuilder,
            IMutation<TSchema, TInput> mutation) where TSchema : ISchema where TInput : class, IInput
        {
            var parameterList = CreateParametersFromInput<TInput>();
            var token = BuildTokenString(mutation.FunctionName, parameterList);
            
            return new MethodSubBuilder<TBuilder, TSchema>(
                parentBuilder,
                token,
                parameterList,
                mutation.Input
            );
        }

        private static List<IParameter> CreateParametersFromInput<TInput>() where TInput : class, IInput
        {
            var inputType = typeof(TInput);
            var fields = inputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var parameterList = new List<IParameter>();

            foreach (var field in fields)
            {
                var parameter = CreateParameterFromField(field);
                parameterList.Add(parameter);
            }

            return parameterList;
        }

        private static ParameterData CreateParameterFromInfo(ParameterInfo paramInfo)
        {
            var typeAttribute = paramInfo.GetCustomAttribute<GraphQLTypeAttribute>();
            var requiredAttribute = paramInfo.GetCustomAttribute<RequiredAttribute>();

            var typeName = typeAttribute?.TypeName ?? paramInfo.ParameterType.Name;
            if (paramInfo.ParameterType.IsArray)
            {
                typeName = $"[{typeName}]";
            }
            if (requiredAttribute != null)
            {
                typeName += "!";
            }

            return new ParameterData(paramInfo.Name, typeName);
        }

        private static ParameterData CreateParameterFromField(FieldInfo field)
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

            var name = jsonAttribute?.PropertyName ?? field.Name;
            return new ParameterData(name, typeName);
        }

        private static string BuildTokenString(string methodName, List<IParameter> parameters)
        {
            var args = string.Join(", ", parameters.Select(GetParameterString));
            return args.Length == 0 ? methodName : $"{methodName} ({args})";
        }

        private static string GetParameterString(IParameter parameter)
        {
            var name = parameter.ParameterName;
            if (name.EndsWith("_input"))
            {
                name = "input";
            }
            return $"{name}: ${parameter.ParameterName}";
        }
    }
}