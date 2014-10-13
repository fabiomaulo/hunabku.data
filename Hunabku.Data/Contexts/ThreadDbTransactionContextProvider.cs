using System;

namespace Hunabku.Data.Contexts
{
	/// <summary>
	/// Context, manejado completamente por un clase externa, que garantiza un contexto per thread.
	/// </summary>
	/// <remarks>
	/// Las clases que pidan el current context recibirán una instancia no-disposable para garantizar que el completo
	/// control del current context sea de la clase que creó el <see cref="ThreadDbTransactionContextProvider">provider</see>.
	/// </remarks>
	public class ThreadDbTransactionContextProvider : IDbTransactionContextProvider
	{
		private class BindedDbTransactionContext : CommandActionDbTransactionContext
		{
			private readonly ThreadDbTransactionContextProvider owner;

			public BindedDbTransactionContext(ThreadDbTransactionContextProvider owner, bool readOnly = false)
				: base(readOnly)
			{
				this.owner = owner;
			}

			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				owner.Unbind();
			}
		}

		[ThreadStatic]
		private static IDbTransactionContext currentDbTransactionContext;

		public IDbTransactionContext Binded(bool readOnly = false)
		{
			if (currentDbTransactionContext != null)
			{
				throw new InvalidOperationException("No puede bindear dos IDbTransactionContext conteporaneamente.");
			}

			currentDbTransactionContext = new BindedDbTransactionContext(this, readOnly);
			return currentDbTransactionContext;
		}

		public void Bind(IDbTransactionContext currentContext)
		{
			if (currentContext == null) { throw new ArgumentNullException("currentContext"); }
			if (currentDbTransactionContext != null)
			{
				throw new InvalidOperationException("No puede bindear dos IDbTransactionContext conteporaneamente.");
			}
			currentDbTransactionContext = currentContext;
		}

		public void Unbind()
		{
			currentDbTransactionContext = null;
		}

		public IDbTransactionContext GetCurrentContext()
		{
			if (currentDbTransactionContext == null)
			{
				throw new InvalidOperationException("No fue definido ningún contexto de transacción con RDBMS.");
			}
			return new NoDisposableWrapperDbTransactionContext(currentDbTransactionContext);
		}
	}
}