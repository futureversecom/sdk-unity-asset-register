// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegister.Runtime.Utils
{
	internal abstract class AbstractConverter<T> : JsonConverter
    {
        private static readonly Dictionary<Type, Dictionary<string, Type>> s_typeMaps = new();

        public override bool CanConvert(Type objectType)
        {
            return typeof(IUnion).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (!typeof(T).IsAssignableFrom(objectType))
                {
                    throw new JsonSerializationException($"{objectType.Name} must implement {typeof(T).Name} to be used with this converter.");
                }
                
                var jo = JObject.Load(reader);
                var typeName = jo["__typename"]?.ToString();

                if (string.IsNullOrEmpty(typeName))
                {
                    throw new JsonSerializationException($"Missing '__typename' field for {typeof(T).Name} deserialization.");
                }

                if (!s_typeMaps.TryGetValue(objectType, out var typeMap))
                {
                    typeMap = CreateTypeMap(objectType);
                    s_typeMaps[objectType] = typeMap;
                }

                if (!typeMap.TryGetValue(typeName, out var targetType))
                {
                    throw new JsonSerializationException($"Unknown __typename '{typeName}' for type '{objectType.Name}'.");
                }

                return jo.ToObject(targetType, serializer)!;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private static Dictionary<string, Type> CreateTypeMap(Type baseType)
        {
            var derivedTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    baseType.IsAssignableFrom(t)
                    && !t.IsAbstract
                    && !t.IsInterface)
                .ToList();

            var map = new Dictionary<string, Type>();
            foreach (var type in derivedTypes)
            {
                map[type.Name] = type;
            }

            return map;
        }
    }
    
    internal class UnionConverter : AbstractConverter<IUnion> { }
    internal class InterfaceConverter : AbstractConverter<IInterface> { }
}