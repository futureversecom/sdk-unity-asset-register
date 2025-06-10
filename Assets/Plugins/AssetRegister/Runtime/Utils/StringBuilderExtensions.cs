// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Text;

namespace Plugins.AssetRegister.Runtime.Utils
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendIndented(this StringBuilder builder, string text, int indent)
		{
			return builder.Append(new string(' ', indent * 2)).Append(text);
		}
		
		public static StringBuilder AppendLineIndented(this StringBuilder builder, string text, int indent)
		{
			return builder.Append(new string(' ', indent * 2)).AppendLine(text);
		}
	}
}