using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hunabku.Data
{
	public class SetValuesClause
	{
		private readonly List<Tuple<string, QueryParameter>> fieldsValues = new List<Tuple<string, QueryParameter>>();

		public void Add(string fieldName, object value)
		{
			var qp = new QueryParameter(GetParameterNameFor(fieldName), value);
			fieldsValues.Add(new Tuple<string, QueryParameter>(fieldName, qp));
		}

		private string GetParameterNameFor(string fieldName)
		{
			return string.Concat("@", fieldName, "_");
		}

		public string UpdateClause()
		{
			// se puede hacer con un StringBuilder pero... una linea es una linea y hay que explicarla menos
			return string.Join(", ", fieldsValues.Select(x => string.Format("{0} = {1}", x.Item1, x.Item2.ParameterName)));
		}

		public string InsertClause()
		{
			string columns = string.Join(", ", fieldsValues.Select(x => x.Item1));
			string valuesParameters = string.Join(", ", fieldsValues.Select(x => x.Item2.ParameterName));
			return string.Concat("(", columns, ") VALUES (", valuesParameters, ")");
		}

		public IEnumerable<QueryParameter> Parameters()
		{
			return fieldsValues.Select(x => x.Item2).DistinctByName();
		}

		public IEnumerable<IDbDataParameter> Parameters(IDbCommand command)
		{
			return Parameters().ToDbParameters(command);
		}

		public override string ToString()
		{
			return UpdateClause();
		}
	}
}