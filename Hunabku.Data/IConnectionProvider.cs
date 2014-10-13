using System.Collections.Generic;
using System.Data;

namespace Hunabku.Data
{
	public interface IConnectionProvider
	{
		/// <summary>
		/// Initialize the connection provider from the given properties.
		/// </summary>
		/// <param name="settings">The connection provider settings</param>
		void Configure(IDictionary<string, string> settings);

		/// <summary>
		/// Get an open <see cref="IDbConnection"/>.
		/// </summary>
		/// <returns>An open <see cref="IDbConnection"/>.</returns>
		IDbConnection GetConnection(); 
	}
}