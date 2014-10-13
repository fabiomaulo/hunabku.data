using System.Data;

namespace Hunabku.Data
{
	/// <summary>
	/// Provee el transaction context en el marco del cual se pueden ejecutar una serie de <see cref="IDbCommand"/>.
	/// </summary>
	/// <remarks>
	/// En general una clase de servicio puede crear un <see cref="IDbTransactionContext"/> en el marco del cual
	/// se ejecutan todos los acceso a DB que el metodo del servicio mismo necesita.
	/// Tambien, para commands que se ejecutan asincronico en su propio thread (un thread por action) será posible
	/// implementar un <see cref="IDbTransactionContextProvider"/> para el entero thread así como es posible
	/// un <see cref="IDbTransactionContextProvider"/> per request context (en WEB).
	/// </remarks>
	public interface IDbTransactionContextProvider
	{
		IDbTransactionContext GetCurrentContext();
	}
}