// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq.Expressions;
using AssetRegister.Runtime.Interfaces;
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
                var value = BuilderUtils.GetValueFromExpression(expression.Arguments[i]);
                
                // Ignore parameter if the value is default
                if (value == null || value.Equals(BuilderUtils.GetDefaultValue(value.GetType())))
                {
                    continue;
                }
                
                var paramInfo = parameters[i];
                var parameter = BuilderUtils.CreateParameterFromInfo(paramInfo);

                input.Add(parameter.ParameterName, value);
                parameterList.Add(parameter);
            }

            var token = BuilderUtils.BuildTokenString(method.Name, parameterList);
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
            var parameterList = BuilderUtils.CreateParametersFromInput(query.Input);
            var token = BuilderUtils.BuildTokenString(query.QueryName, parameterList);
            
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
            var parameterList = BuilderUtils.CreateParametersFromInput(mutation.Input);
            var token = BuilderUtils.BuildTokenString(mutation.FunctionName, parameterList);
            
            return new MethodSubBuilder<TBuilder, TSchema>(
                parentBuilder,
                token,
                parameterList,
                mutation.Input
            );
        }
    }
}