using System;
using Npgsql;

namespace BachelorOppgaveBackend
{
    public class AzurePostgres
    {

        private NpgsqlConnectionStringBuilder connStr;

        public AzurePostgres() { ConnectToDb(); }
        public void ConnectToDb()
        {
            // Replace <cluster> with your cluster name and <password> with your password:
            connStr = new NpgsqlConnectionStringBuilder("Server = c.sg1be7af508fe242089624321e953c0f3c.postgres.database.azure.com; Database = citus; Port = 5432; User Id = citus; Password = Vjgxollgrnlkm12; Ssl Mode = Require; Pooling = true; Minimum Pool Size=0; Maximum Pool Size =50 ");
            connStr.TrustServerCertificate = true;
        }

        public NpgsqlConnection Connect()
        {
            return new NpgsqlConnection(connStr.ToString());
        }

        public void CreateTable()
        {

        }

        public void DropTable()
        {

        }

        public void CreateIndex()
        {

        }

        public void Insert()
        {

        }
    }
}


//using (var conn = new NpgsqlConnection(connStr.ToString()))
//{
//    Console.Out.WriteLine("Opening connection");
//    conn.Open();
//    using (var command = new NpgsqlCommand("DROP TABLE IF EXISTS pharmacy;", conn))
//    {
//        command.ExecuteNonQuery();
//        Console.Out.WriteLine("Finished dropping table (if existed)");
//    }
//    using (var command = new NpgsqlCommand("CREATE TABLE pharmacy (pharmacy_id integer ,pharmacy_name text,city text,state text,zip_code integer);", conn))
//    {
//        command.ExecuteNonQuery();
//        Console.Out.WriteLine("Finished creating table");
//    }
//    using (var command = new NpgsqlCommand("CREATE INDEX idx_pharmacy_id ON pharmacy(pharmacy_id);", conn))
//    {
//        command.ExecuteNonQuery();
//        Console.Out.WriteLine("Finished creating index");
//    }
//    using (var command = new NpgsqlCommand("INSERT INTO  pharmacy  (pharmacy_id,pharmacy_name,city,state,zip_code) VALUES (@n1, @q1, @a, @b, @c)", conn))
//    {
//        command.Parameters.AddWithValue("n1", 0);
//        command.Parameters.AddWithValue("q1", "Target");
//        command.Parameters.AddWithValue("a", "Sunnyvale");
//        command.Parameters.AddWithValue("b", "California");
//        command.Parameters.AddWithValue("c", 94001);
//        int nRows = command.ExecuteNonQuery();
//        Console.Out.WriteLine(String.Format("Number of rows inserted={0}", nRows));
//    }

//}
//Console.WriteLine("Press RETURN to exit");
//Console.ReadLine();
//        }