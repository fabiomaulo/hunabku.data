using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Hunabku.Data
{
	public class SqlConnectionProvider: IConnectionProvider
	{
		private string connString;

		public void Configure(IDictionary<string, string> settings)
		{
			// cualquier dato se necesite configurar se puede pasar en settings que a su vez puede venir desde cualquier lado
			// desde el app.config, los settings de la aplicación o del usuario, desde un string fijo en RAM etc.
			connString = "Connection String No Configurada";
			string connectionStringName;
			if (settings.TryGetValue("ConnectionStringName", out connectionStringName))
			{
				var conString = (from ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings
												 where connectionStringName.Equals(connectionString.Name)
												 select connectionString.ConnectionString).FirstOrDefault();
				if (conString != null)
				{
					var connectionStringBuilder = new SqlConnectionStringBuilder(conString);
					connString = connectionStringBuilder.ConnectionString;
				}
			}
		}
		protected virtual string ConnectionString
		{
			get
			{
				if (string.IsNullOrWhiteSpace(connString))
				{
					throw new InvalidOperationException("El string de conexión con el WebGet no fue configurado.");
				}
				return connString;
			}
		}

		public IDbConnection GetConnection()
		{
			IDbConnection conn = new SqlConnection();
			try
			{
				conn.ConnectionString = ConnectionString;
				conn.Open();
			}
			catch (Exception)
			{
				conn.Dispose();
				throw;
			}

			return conn;
		}
	}
}