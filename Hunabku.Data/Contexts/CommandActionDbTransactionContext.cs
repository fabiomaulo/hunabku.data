using System;
using System.Data;

namespace Hunabku.Data.Contexts
{
	public class CommandActionDbTransactionContext : IDbTransactionContext
	{
		private readonly Lazy<IDbConnection> lazyConnection;
		private readonly Lazy<IDbTransaction> lazyTransaction;

		public CommandActionDbTransactionContext(bool readOnly = false) : this(ConnectionProviderFactory.Current, readOnly) { }

		public CommandActionDbTransactionContext(IConnectionProvider provider, bool readOnly)
		{
			if (provider == null) { throw new ArgumentNullException("provider"); }
			IsReadOnly = readOnly;
			lazyConnection = new Lazy<IDbConnection>(()=> provider.GetConnection());
			lazyTransaction = new Lazy<IDbTransaction>(()=> Connection.BeginTransaction());
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// NOTE: Leave out the finalizer altogether if this class doesn't 
		// own unmanaged resources itself, but leave the other methods
		// exactly as they are. 
		~CommandActionDbTransactionContext()
		{
			// Finalizer calls Dispose(false)
			Dispose(false);
		}

		// The bulk of the clean-up code is implemented in Dispose(bool)
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				if (lazyTransaction.IsValueCreated)
				{
					var transaction = Transaction;
					if (!IsReadOnly)
					{
						transaction.Commit();
					}
					else
					{
						transaction.Rollback();
					}
					transaction.Dispose();
				}
				if (lazyConnection.IsValueCreated)
				{
					Connection.Dispose();
				}
			}
		}

		public IDbConnection Connection
		{
			get { return lazyConnection.Value; }
		}

		public IDbTransaction Transaction
		{
			get { return lazyTransaction.Value; }
		}

		public IDbCommand CreateCommand()
		{
			var command = Connection.CreateCommand();
			command.Transaction = Transaction;
			return command;
		}

		public bool IsReadOnly { get; private set; }
	}
}