using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hunabku.Data.Predicates;

namespace Hunabku.Data
{
	public class WhereAndClause : IWhereClause
	{
		private readonly List<IWherePredicate> whereAndClauses = new List<IWherePredicate>();

		public void And(string predicate, QueryParameter parameterValue)
		{
			whereAndClauses.Add(new PredicateWithParameter { Predicate = predicate, Parameter = parameterValue });
		}

		public void AndIn(string fieldPath, object[] values)
		{
			whereAndClauses.Add(new InPredicate { FieldPath = fieldPath, Values = values });
		}

		public void And(string predicate)
		{
			whereAndClauses.Add(new PredicateOnly { Predicate = predicate });
		}

		public void And(string predicate, IEnumerable<QueryParameter> parametersValue)
		{
			whereAndClauses.Add(new PredicateWithParameters { Predicate = predicate, Parameters = parametersValue });
		}

		public void And(IWhereClause predicate)
		{
			whereAndClauses.Add(new PredicateClause { Clause = predicate });
		}

		public string Clause()
		{
			return string.Join(" and ", whereAndClauses.Select(x => x.Predicate()).Where(x => !string.IsNullOrEmpty(x)).ToArray());
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			return whereAndClauses
				.SelectMany(x => x.Parameters()).DistinctByName();
		}

		public IEnumerable<IDbDataParameter> Parameters(IDbCommand command)
		{
			return Parameters().ToDbParameters(command);
		}

		public override string ToString()
		{
			return Clause();
		}
	}
}