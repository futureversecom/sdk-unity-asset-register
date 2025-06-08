// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Text;

namespace Plugins.AssetRegisterV2.Runtime
{
	public static class StringUtils
	{
		public static StringBuilder AppendIndented(this StringBuilder builder, int indentation, string text)
		{
			return builder.Append(new string(' ', indentation)).Append(text);
		}
		
		public static StringBuilder AppendLineIndented(this StringBuilder builder, int indentation, string text)
		{
			return builder.Append(new string(' ', indentation)).AppendLine(text);
		}
	}
}