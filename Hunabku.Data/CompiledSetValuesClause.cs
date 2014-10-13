using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hunabku.Data
{
	public class CompiledSetValuesClause<TSubject> : IEnumerable<KeyValuePair<string, Func<TSubject, object>>>
	{
		private class SetSpecification
		{
			public string FieldName { get; set; }
			public string ParameterName { get; set; }
			public Func<TSubject, object> ValueGetter { get; set; }
		}

		private readonly List<KeyValuePair<string, Func<TSubject, object>>> fieldsValues = new List<KeyValuePair<string, Func<TSubject, object>>>();
		private readonly Lazy<string> updateClause;
		private readonly Lazy<string> insertClause;
		private readonly Lazy<List<SetSpecification>> specifications;

		public CompiledSetValuesClause()
		{
			specifications = new Lazy<List<SetSpecification>>(() => fieldsValues.Select(x => new SetSpecification { FieldName = x.Key, ParameterName = GetParameterNameFor(x.Key), ValueGetter = x.Value }).ToList());
			updateClause = new Lazy<string>(() => string.Join(", ", specifications.Value.Select(x => string.Format("{0} = {1}", x.FieldName, x.ParameterName))));
			insertClause = new Lazy<string>(() =>
			{
				string columns = string.Join(", ", specifications.Value.Select(x => x.FieldName));
				string valuesParameters = string.Join(", ", specifications.Value.Select(x => x.ParameterName));
				return string.Concat("(", columns, ") VALUES (", valuesParameters, ")");
			});
		}

		private void Add(KeyValuePair<string, Func<TSubject, object>> specification)
		{
			if (string.IsNullOrWhiteSpace(specification.Key))
			{
				throw new ArgumentNullException("specification");
			}
			if (specification.Value == null)
			{
				throw new ArgumentNullException("specification", "Parameter value getter not provided.");
			}
			var wasInitialized = specifications.IsValueCreated || updateClause.IsValueCreated || insertClause.IsValueCreated;
			if (wasInitialized)
			{
				throw new InvalidOperationException("The collection is readonly after its first usage.");
			}
			fieldsValues.Add(specification);
		}

		public void Add(string fieldName, Func<TSubject, object> valueGetter)
		{
			if (string.IsNullOrWhiteSpace(fieldName))
			{
				throw new ArgumentNullException("fieldName");
			}
			if (valueGetter == null)
			{
				throw new ArgumentNullException("valueGetter", "Parameter value getter not provided.");
			}
			Add(new KeyValuePair<string, Func<TSubject, object>>(fieldName, valueGetter));
		}

		private string GetParameterNameFor(string fieldName)
		{
			return string.Concat("@", fieldName, "_");
		}

		public string UpdateClause()
		{
			return updateClause.Value;
		}

		public string InsertClause()
		{
			return insertClause.Value;
		}

		public IEnumerable<IDbDataParameter> Parameters(IDbCommand command, TSubject source)
		{
			return specifications.Value.Select(x =>
			{
				var parameter = command.CreateParameter();
				parameter.ParameterName = x.ParameterName;
				parameter.Value = x.ValueGetter(source);
				return parameter;
			});
		}

		public IEnumerator<KeyValuePair<string, Func<TSubject, object>>> GetEnumerator()
		{
			return fieldsValues.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return UpdateClause();
		}
	}
}