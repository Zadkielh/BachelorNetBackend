using System;
using System.Configuration;
using System.Collections.Specialized;
using Npgsql;
using BachelorOppgaveBackend.Model;


// https://learn.microsoft.com/en-us/azure/cosmos-db/postgresql/quickstart-app-stacks-csharp
namespace BachelorOppgaveBackend
{   
    public class AzurePostgresConfigs
    {
        public string server = "c.sg6222938976534e0d862d260256f19fda.postgres.database.azure.com";
        public string databaseType = "citus";
        public string port = "5432";
        public string userId = "citus";
        public string password = "Mosabjonn1814";
        public string ssl = "Require";
        public string pooling = "true";
        public string minPoolSize = "0";
        public string maxPoolSize = "50";

    }
    public class AzurePostgres
    {

        private NpgsqlConnectionStringBuilder connStr;
        private NpgsqlConnection conn;

       
        public AzurePostgres() {
            var pgConfig = new AzurePostgresConfigs();
            connStr = new NpgsqlConnectionStringBuilder($"Server = {pgConfig.server}; Database = {pgConfig.databaseType}; Port = {pgConfig.port}; User Id = {pgConfig.userId}; Password = {pgConfig.password}; Ssl Mode = {pgConfig.ssl}; Pooling = {pgConfig.pooling}; Minimum Pool Size={pgConfig.maxPoolSize}; Maximum Pool Size ={pgConfig.maxPoolSize}");
            connStr.TrustServerCertificate = true;
            conn = new NpgsqlConnection(connStr.ToString());  
        }

        public NpgsqlConnection getConn() {
            return conn;
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

            var userCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Users (id uuid ,user_name text,created_at timestamp not null default CURRENT_TIMESTAMP ,user_role_id integer);", conn);
            var userRoleCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS User_Role (id uuid ,user_role_type text,description text ,created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var voteCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Vote (id uuid ,user_id integer ,post_id integer, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var statusCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Status (id uuid ,post_id integer, user_id integer, status text, description text ,created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var postCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Post (id uuid ,title text,description text,user_id integer, category_id integer, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);
            var commentCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Comment (id uuid ,post_id integer,parent_id integer,user_id integer, content text, created_at timestamp not null default CURRENT_TIMESTAMP );", conn);
            var categoryCommand = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Category (id uuid ,name text, description text, created_at timestamp not null default CURRENT_TIMESTAMP);", conn);

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

        public int AddUser(Guid uuid, string user_name, int user_role_id)
        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            var userCommand = new NpgsqlCommand("INSERT INTO  User  (id,user_name,user_role_id) VALUES (@a, @b, @c)", conn);
            userCommand.Parameters.AddWithValue("a", uuid);
            userCommand.Parameters.AddWithValue("b", user_name);
            userCommand.Parameters.AddWithValue("c", user_role_id);
            var res = userCommand.ExecuteNonQuery();

            conn.Close();
            return res;
        }

        public async IAsyncEnumerable<User> GetUsers() 
        {
            Console.Out.WriteLine("GetUsers");
            conn.Open();
            await using var q = new NpgsqlCommand("SELECT * FROM Users", conn);
            await using var reader = await q.ExecuteReaderAsync();
            


            while (reader.Read()) {
                var d = new User();
                d.id = reader.GetGuid(0);
                d.user_name = reader.GetString(1);
                d.created_at = reader.GetDateTime(2);
                d.user_role_id = reader.GetInt32(3);
                
                yield return d;
            }
            conn.Close();
            reader.Close();
        }

        public async IAsyncEnumerable<User> GetUser(Guid id) {
            Console.Out.WriteLine("GetUser");
            conn.Open();
            await using var q = new NpgsqlCommand("SELECT * FROM Users WHERE user_id = @a", conn);
            q.Parameters.AddWithValue("a", id);
            await using var reader = await q.ExecuteReaderAsync();

            while(reader.Read()) {
                 var d = new User();
                d.id = reader.GetGuid(0);
                d.user_name = reader.GetString(1);
                d.created_at = reader.GetDateTime(2);
                d.user_role_id = reader.GetInt32(3);
                
                yield return d;
            }
            conn.Close();
            reader.Close();
        }


        public NpgsqlDataReader GetAny(string table) {
            var q = new NpgsqlCommand("SELECT * FROM " + table, conn);
            return q.ExecuteReader();
        }
    }

    
}