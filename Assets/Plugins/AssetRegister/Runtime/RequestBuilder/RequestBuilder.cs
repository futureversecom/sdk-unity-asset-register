// Copyright (c) 2025, Futureverse Corporation Limited. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.AssetRegister.Runtime.Interfaces;

namespace Plugins.AssetRegister.Runtime.Requests
{
	public abstract class RequestBuilder<TData> where TData : IQueryData
	{
		protected readonly List<TData> RequestData = new();

		internal RequestBuilder<TData> RegisterQuery(TData data)
		{
			RequestData.Add(data);
			return this;
		}

		public abstract Request Build();
		
		protected static string BuildModelString(TData data, bool includeParams)
		{
			var stringBuilder = new StringBuilder();
			BuildFieldsStringRecursive(ref stringBuilder, data.RootNode, includeParams ? data.Parameters : null);
			return stringBuilder.ToString();
		}
		
		private static void BuildFieldsStringRecursive(ref StringBuilder builder, FieldTreeNode fieldNode, List<ParameterInfo> parameters = null)
		{
			builder.AppendLine(fieldNode.FieldName);

			if (parameters != null)
			{
				builder.Append("(");
				builder.Append(
					string.Join(",", parameters.Select(p => $"{p.ParameterName}: ${p.ParameterName}"))
				);
				builder.Append(")");
			}
			
			if (fieldNode.Children.Count == 0)
			{
				return;
			}

			builder.AppendLine("{");
			foreach (var child in fieldNode.Children)
			{
				BuildFieldsStringRecursive(ref builder, child);
			}
			builder.AppendLine("}");
		}
		
#if USING_UNITASK
		public async UniTask<Result> 
#else 
		public IEnumerator
#endif
			Execute(
				IAssetRegisterClient client,
				string authenticationToken = null,
#if USING_UNITASK
				CancellationToken cancellationToken = default
#else
				Action<QueryResult> onComplete = null
#endif
			)
		{
			var queryObject = Build();
#if USING_UNITASK
			return await client.MakeRequest(queryObject, authenticationToken, cancellationToken);
#else
			yield return client.Query(queryObject, authenticationToken, onComplete);
#endif
		}
	}
}