using Npgsql;


namespace BachelorOppgaveBackend.Model
{
    public class User
    {
        public Guid id { get; set; }

        public string? user_name { get; set; }

        public DateTime created_at { get; set; }

        public int user_role_id { get; set; }    
    }


    public class DbUser {
        private NpgsqlConnection conn;

        public DbUser(NpgsqlConnection Setconn) {
            conn = Setconn;
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
    }
}

