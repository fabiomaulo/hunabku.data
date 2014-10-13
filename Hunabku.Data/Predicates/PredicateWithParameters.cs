using System.Collections.Generic;
using System.Linq;

namespace Hunabku.Data.Predicates
{
	/// <summary>
	/// Predicate multi parametros
	/// </summary>
	public class PredicateWithParameters : IWherePredicate
	{
		public string Predicate { get; set; }
		public IEnumerable<QueryParameter> Parameters { get; set; }

		string IWherePredicate.Predicate()
		{
			return string.IsNullOrEmpty(Predicate) ? "" : string.Concat("(", Predicate, ")");
		}

		IEnumerable<QueryParameter> IWherePredicate.Parameters()
		{
			return Parameters ?? Enumerable.Empty<QueryParameter>();
		}
	}
}