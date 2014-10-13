using System.Collections.Generic;

namespace Hunabku.Data
{
	public interface IWherePredicate
	{
		string Predicate();
		IEnumerable<QueryParameter> Parameters();
	}
}