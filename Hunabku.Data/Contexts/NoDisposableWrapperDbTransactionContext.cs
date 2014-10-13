using System;
using System.Data;

namespace Hunabku.Data.Contexts
{
	public class NoDisposableWrapperDbTransactionContext : IDbTransactionContext
	{
		private readonly IDbTransactionContext context;

		public NoDisposableWrapperDbTransactionContext(IDbTransactionContext context)
		{
			if (context == null) { throw new ArgumentNullException("context"); }
			this.context = context;
		}

		public void Dispose()
		{
			// nop
		}

		public IDbConnection Connection
		{
			get { return context.Connection; }
		}

		public IDbTransaction Transaction
		{
			get { return context.Transaction; }
		}

		public IDbCommand CreateCommand()
		{
			return context.CreateCommand();
		}

		public bool IsReadOnly
		{
			get { return context.IsReadOnly; }
		}
	}
}