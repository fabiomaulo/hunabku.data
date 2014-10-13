using System;

namespace Hunabku.Data
{
	public class QueryParameter
	{
		public QueryParameter(string parameterName, object value)
		{
			if (parameterName == null) throw new ArgumentNullException("parameterName");
			ParameterName = parameterName;
			Value = value;
		}

		public static QueryParameter New(string parameterName, object value)
		{
			return new QueryParameter(parameterName, value);
		}

		public string ParameterName { get; private set; }
		public object Value { get; private set; } 
	}
}