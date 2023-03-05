using Npgsql;


namespace BachelorOppgaveBackend.Model
{
    public class User
    {
        public Guid id { get; set; }

        public string? user_name { get; set; }

        public DateTime created_at { get; set; }

        public Guid user_role_id { get; set; }    
    }


    public class DbUser {
        private NpgsqlConnection conn;

        public DbUser(NpgsqlConnection Setconn) {
            conn = Setconn;
        }

        public int AddUser(Guid uuid, string user_name, Guid user_role_id)
        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            var userCommand = new NpgsqlCommand("INSERT INTO Users (id,user_name,user_role_id) VALUES (@a, @b, @c)", conn);
            userCommand.Parameters.AddWithValue("a", uuid);
            userCommand.Parameters.AddWithValue("b", user_name);
            userCommand.Parameters.AddWithValue("c", user_role_id);
            var res = userCommand.ExecuteNonQuery();

            conn.Close();
            return res;
        }


        public async IAsyncEnumerable<User> GetUser(Guid id) {
            Console.Out.WriteLine("GetUser");
            conn.Open();
            await using var q = new NpgsqlCommand("SELECT * FROM Users WHERE id = @a", conn);
            q.Parameters.AddWithValue("a", id);
            await using var reader = await q.ExecuteReaderAsync();

            while(reader.Read()) {
                 var d = new User();
                d.id = reader.GetGuid(0);
                d.user_name = reader.GetString(1);
                d.created_at = reader.GetDateTime(2);
                d.user_role_id = reader.GetGuid(3);
                
                yield return d;
            }
            conn.Close();
            reader.Close();
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
                d.user_role_id = reader.GetGuid(3);
                
                yield return d;
            }
            conn.Close();
            reader.Close();
        }
    }
}

