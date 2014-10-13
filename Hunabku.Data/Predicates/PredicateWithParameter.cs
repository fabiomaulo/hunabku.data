using System.Collections.Generic;

namespace Hunabku.Data.Predicates
{
	/// <summary>
	/// Predicato con un parametro.
	/// </summary>
	/// <example>
	/// "t1.Field1 = @param1"
	/// </example>
	public class PredicateWithParameter : IWherePredicate
	{
		public string Predicate { get; set; }
		public QueryParameter Parameter { get; set; }

		string IWherePredicate.Predicate()
		{
			return string.IsNullOrEmpty(Predicate) ? "" : string.Concat("(", Predicate, ")");
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			yield return Parameter;
		}
	}
}