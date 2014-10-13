using System;
using System.Collections.Generic;

namespace Hunabku.Data
{
	public class ConnectionProviderFactory
	{
		private static IConnectionProvider current;
		private static readonly object syncObj = new object();

		/// <summary>
		/// Connection provider común a toda la applicación.
		/// </summary>
		/// <remarks>
		///  Si se usa el current se asume que no hay diferentes consfiguraciones por thread.
		/// </remarks>
		public static IConnectionProvider Current
		{
			get { return current; }
			set
			{
				var prev = current;
				lock (syncObj)
				{
					if (prev != null && value != null)
					{
						throw new InvalidOperationException("No es posible cambiar la configuración del connection provider.");
					}
				}
				current = value;
			}
		}

		public static IConnectionProvider NewConnectionProvider(IDictionary<string, string> settings)
		{
			/* 
			 * En este caso solo nos conectamos via SQLServer así que el connectionProvider es bien conocido.
			 * Desde los settings se podría conocer la clase concreta de IConnectionProvider, instanciarla con el Activator
			 * y luego usarla en el programa.
			*/
			IConnectionProvider connections = new SqlConnectionProvider();
			connections.Configure(settings);
			return connections;
		} 
	}
}