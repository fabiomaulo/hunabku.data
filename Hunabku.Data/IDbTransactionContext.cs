using System;
using System.Data;

namespace Hunabku.Data
{
	/// <summary>
	/// Contexto de ejecución de commands a RDBMS.
	/// </summary>
	/// <remarks>
	/// Los usuarios de esta implementación pueden leer sea la connection que la transaction pero
	/// no deberían jamás operar sobre ellas cambiando su estado (ni close y menos dispose).
	/// </remarks>
	public interface IDbTransactionContext : IDisposable
	{
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; }

		/// <summary>
		/// Crea un nuevo see <see cref="IDbCommand"/> asociado con la <see cref="Connection"/> y la <see cref="Transaction"/>.
		/// </summary>
		/// <returns>Un nuevo <see cref="IDbCommand"/>.</returns>
		IDbCommand CreateCommand();

		/// <summary>
		/// Define si el context se está ejecuntando en un marco readonly. En tal caso no se efectuarán modificaciones al DB.
		/// </summary>
		bool IsReadOnly { get; }
	}
}