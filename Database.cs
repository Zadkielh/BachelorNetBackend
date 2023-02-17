using System;
using System.Configuration;
using System.Collections.Specialized;
using Npgsql;

using namespc = System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


// https://learn.microsoft.com/en-us/azure/cosmos-db/postgresql/quickstart-app-stacks-csharp
namespace BachelorOppgaveBackend
{
    public class AzurePostgres
    {

        private NpgsqlConnectionStringBuilder connStr;
        private NpgsqlConnection conn;

        public AzurePostgres() {
            connStr = new NpgsqlConnectionStringBuilder("Server = c.sg1be7af508fe242089624321e953c0f3c.postgres.database.azure.com; Database = citus; Port = 5432; User Id = citus; Password = Mosabjonn1814; Ssl Mode = Require; Pooling = true; Minimum Pool Size=0; Maximum Pool Size =50");
            connStr.TrustServerCertificate = true;
            conn = new NpgsqlConnection(connStr.ToString());

            Validate();
            
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

        public void Validate()
        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            var userCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Users (user_id uuid ,user_name text,created_at timestamp not null default CURRENT_TIMESTAMP ,user_role_id integer);", conn);
            var userRoleCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS User_Role (user_role_id integer ,user_role_type text,description text ,created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var voteCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Vote (vote_id integer ,user_id integer ,post_id integer, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var statusCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Status (status_id integer ,post_id integer, user_id integer, status text, description text ,created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var postCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Post (post_id integer ,title text,description text,user_id integer, category_id integer, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var commentCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Comment (comment_id integer ,post_id integer,parent_id integer,user_id integer, content text, created_at timestamp not null default CURRENT_TIMESTAMP );", conn);
            var categoryCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Category (category_id integer ,name text, description text, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);

            userCommand.ExecuteNonQuery();
            userRoleCommand.ExecuteNonQuery();
            voteCommand.ExecuteNonQuery();
            statusCommand.ExecuteNonQuery();
            postCommand.ExecuteNonQuery();
            commentCommand.ExecuteNonQuery();
            categoryCommand.ExecuteNonQuery();

            Console.Out.WriteLine("Finished validating.");

            conn.Close();
        }

        public void AddUser(Guid uuid, string user_name, int user_role_id)
        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            var userCommand = new NpgsqlCommand("INSERT INTO  Users  (user_id,user_name,user_role_id) VALUES (@a, @b, @c)", conn);
            userCommand.Parameters.AddWithValue("a", uuid);
            userCommand.Parameters.AddWithValue("b", user_name);
            userCommand.Parameters.AddWithValue("c", user_role_id);
            userCommand.ExecuteNonQuery();

            conn.Close();
        }

    }
}