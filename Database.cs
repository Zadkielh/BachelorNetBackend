using System;
using System.Configuration;
using System.Collections.Specialized;
using Npgsql;

using namespc = System.Configuration;


// https://learn.microsoft.com/en-us/azure/cosmos-db/postgresql/quickstart-app-stacks-csharp
namespace BachelorOppgaveBackend
{
    public class AzurePostgres
    {

        private NpgsqlConnectionStringBuilder connStr;
        private NpgsqlConnection conn;

        public AzurePostgres() { Setup(); }
        public void Setup()
        {

            var connectionString = namespc.ConfigurationManager.ConnectionStrings["CosmosDBUserFeedBack"].ConnectionString;

            connStr = new NpgsqlConnectionStringBuilder(connectionString);
            connStr.TrustServerCertificate = true;
            conn = new NpgsqlConnection(connStr.ToString());
        }

        public void Reconnect()
        {
            conn = new NpgsqlConnection(connStr.ToString());
        }

        public void PerformCommand(string query)
        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            var command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();
            Console.Out.WriteLine("Finished running command.");
        }

    }
}