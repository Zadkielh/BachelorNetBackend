namespace BachelorOppgaveBackend.Model
{
    public class User
    {
        public Guid user_id { get; set; }

        public string? user_name { get; set; }
        public DateOnly created_at { get; set; }

        public int user_role_id { get; set; }

    }
}
