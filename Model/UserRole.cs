namespace BachelorOppgaveBackend.Model
{
    public class UserRole
    {
        public int user_role_id { get; set; }
        public string? user_role_type { get; set; }

        public string? description { get; set; }

        public DateOnly created_at { get; set; }
    }
}
