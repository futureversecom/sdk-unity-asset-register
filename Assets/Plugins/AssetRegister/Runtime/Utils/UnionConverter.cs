// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugins.AssetRegister.Runtime.Utils
{
	public class UnionConverter<TUnion> : JsonConverter where TUnion : IUnion
	{
		private static readonly Dictionary<string, Type> s_typeMap = new();

		static UnionConverter()
		{
			var unionBaseType = typeof(TUnion);
			var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(asm => asm.GetTypes())
				.Where(t => unionBaseType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
				.ToList();

			foreach (var type in derivedTypes)
			{
				var typeName = type.Name;
				s_typeMap[typeName] = type;
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(TUnion).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var typeName = jo["__typename"]?.ToString();

			if (typeName == null)
				throw new JsonException("Missing '__typename' field for union deserialization.");

			if (!s_typeMap.TryGetValue(typeName, out var targetType))
				throw new JsonException($"Unknown __typename '{typeName}' for union type '{typeof(TUnion).Name}'.");

			return jo.ToObject(targetType, serializer)!;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}
}