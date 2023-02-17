namespace BachelorOppgaveBackend.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public string? UserRoleType { get; set; }

        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
