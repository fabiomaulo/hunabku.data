using System.Collections.Generic;

namespace Hunabku.Data.Predicates
{
	/// <summary>
	/// Predicato que incluye una clausola where completa
	/// </summary>
	/// <remarks>
	/// Se usa cuando la clausola where incluye operaciones con AND y OR
	/// </remarks>
	public class PredicateClause : IWherePredicate
	{
		public IWhereClause Clause { get; set; }

		public string Predicate()
		{
			return string.Concat("(", Clause.Clause(), ")");
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			return Clause.Parameters();
		}
	}
}