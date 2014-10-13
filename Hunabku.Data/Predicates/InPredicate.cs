using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hunabku.Data.Predicates
{
	public class InPredicate : IWherePredicate
	{
		public string FieldPath { get; set; }
		public object[] Values { get; set; }

		public string Predicate()
		{
			if (Values == null || Values.Length == 0)
			{
				return "";
			}
			bool esString = Values[0] is string;
			var sb = new StringBuilder();
			sb.Append('(');
			if (Values.Length == 1)
			{
				sb.Append(FieldPath);
				sb.Append(" = ");
				sb.Append(GetSqlValue(esString, Values[0]));
			}
			else
			{
				sb.Append(FieldPath);
				sb.Append(" in (");
				sb.Append(string.Join(", ", Values.Select(x => GetSqlValue(esString, x)).ToArray()));
				sb.Append(')');
			}

			sb.Append(')');
			return sb.ToString();
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			yield break;
		}

		private string GetSqlValue(bool esString, object value)
		{
			return esString
				? string.Concat("'", value, "'")
				: Convert.ToString(value, CultureInfo.InvariantCulture);
		}
	}
}