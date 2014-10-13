using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hunabku.Data
{
	public static class PredicatesExtensions
	{
		public static IEnumerable<QueryParameter> DistinctByName(this IEnumerable<QueryParameter> source)
		{
			if (source == null) throw new ArgumentNullException("source");
			return source.Where(x => x != null).Distinct(new DbParameterNameEqualityComparar());
		}

		public static IEnumerable<IDbDataParameter> ToDbParameters(this IEnumerable<QueryParameter> source, IDbCommand command)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (command == null) throw new ArgumentNullException("command");
			return source.Where(x => x != null).Select(x => x.ToDbParameter(command));
		}

		public static IDbDataParameter ToDbParameter(this QueryParameter source, IDbCommand command)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (command == null) throw new ArgumentNullException("command");
			var parameter = command.CreateParameter();
			parameter.ParameterName = source.ParameterName;
			parameter.Value = source.Value;
			return parameter;
		}

		private class DbParameterNameEqualityComparar : IEqualityComparer<QueryParameter>
		{
			public bool Equals(QueryParameter x, QueryParameter y)
			{
				return Equals(x.ParameterName, y.ParameterName);
			}

			public int GetHashCode(QueryParameter obj)
			{
				return obj.ParameterName.GetHashCode();
			}
		}
	}
}