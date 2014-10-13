using System.Collections.Generic;

namespace Hunabku.Data
{
	public interface IWhereClause
	{
		string Clause();
		IEnumerable<QueryParameter> Parameters(); 
	}
}