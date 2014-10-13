using System.Collections.Generic;

namespace Hunabku.Data.Predicates
{
	/// <summary>
	/// Predicato sin parametros.
	/// </summary>
	/// <example>
	/// "t1.Field1 = t2.Field1"
	/// </example>
	public class PredicateOnly : IWherePredicate
	{
		public string Predicate { get; set; }

		string IWherePredicate.Predicate()
		{
			return string.IsNullOrEmpty(Predicate) ? "" : string.Concat("(", Predicate, ")");
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			yield break;
		}
	}
}