namespace Hunabku.Data.Contexts
{
	/// <summary>
	/// Crea un nuevo <see cref="CommandActionDbTransactionContext"/> cada vez se pida en context corriente.
	/// </summary>
	public class CallDbTransactionContextProvider : IDbTransactionContextProvider
	{
		public IDbTransactionContext GetCurrentContext()
		{
			return new CommandActionDbTransactionContext(readOnly: false);
		}
	}
}