using System.Collections.Generic;
using System.Linq;
using Hunabku.Data.Predicates;

namespace Hunabku.Data
{
	public class WhereOrClause : IWhereClause
	{
		private readonly List<IWherePredicate> whereOrClauses = new List<IWherePredicate>();

		public void Or(string predicate, QueryParameter parameterValue)
		{
			whereOrClauses.Add(new PredicateWithParameter { Predicate = predicate, Parameter = parameterValue });
		}

		public void OrIn(string fieldPath, object[] values)
		{
			whereOrClauses.Add(new InPredicate { FieldPath = fieldPath, Values = values });
		}

		public void Or(string predicate)
		{
			whereOrClauses.Add(new PredicateOnly { Predicate = predicate });
		}

		public void Or(string predicate, IEnumerable<QueryParameter> parametersValue)
		{
			whereOrClauses.Add(new PredicateWithParameters { Predicate = predicate, Parameters = parametersValue });
		}

		public void Or(IWhereClause predicate)
		{
			whereOrClauses.Add(new PredicateClause { Clause = predicate });
		}

		public string Clause()
		{
			return string.Join(" or ", whereOrClauses.Select(x => x.Predicate()).Where(x => !string.IsNullOrEmpty(x)).ToArray());
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			return whereOrClauses
				.SelectMany(x => x.Parameters()).DistinctByName();
		}

		public override string ToString()
		{
			return Clause();
		}
	}
}