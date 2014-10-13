using System.Collections.Generic;
using Hunabku.Data;
using NUnit.Framework;

namespace Hunabku.DataTests.Examples
{
	public class AppStartupExample
	{
		private static void InitializeData()
		{
		/*
		 * En el SqlConnectionProvider está implementada la posibilidad de configuración de la connection
		 * teniendo como parametro de configuración (configuración del IConnectionProvider) el parametro
		 * "ConnectionStringName". Pasando un valor configurado en el app.config o web.config es suficiente para
		 * que SqlConnectionProvider se conecte.
		 * Este metodo, o uno similar, puede ser llamado en los procesos llamados en el app_start.
		 */

#if DEBUG
			var connectionStringName = "DEBUG";
#else
			var connectionStringName = "Prod";
#endif
			var dbSettings = new Dictionary<string, string> { { "ConnectionStringName", connectionStringName } };
			var defaultConnectionProvider = ConnectionProviderFactory.NewConnectionProvider(dbSettings);
			ConnectionProviderFactory.Current = defaultConnectionProvider;
		}
	}
}