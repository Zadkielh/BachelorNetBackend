namespace BachelorOppgaveBackend.Model
{
    public class UserRole
    {
        public Guid id { get; set; }
        public string? user_role_type { get; set; }

        public string? description { get; set; }

        public DateTime created_at { get; set; }
    }
}
