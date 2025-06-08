// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System;
using System.Reflection;
using AssetRegister.Runtime.Interfaces;
using Newtonsoft.Json;

namespace Plugins.AssetRegister.Runtime.Utils
{
	public static class Utils
	{
		public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attribute)
		where TAttribute : Attribute
		{
			attribute = type.GetCustomAttribute<TAttribute>();
			return attribute != null;
		}
		
		public static string GetSchemaName<TSchema>() where TSchema : ISchema
		{
			var type = typeof(TSchema);
			if (type.TryGetAttribute(out JsonObjectAttribute jsonObjectAttribute))
			{
				return jsonObjectAttribute.Id;
			}
			
			return type.Name.ToLower();
		}
	}
}