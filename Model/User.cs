namespace BachelorOppgaveBackend.Model
{
    public class User
    {
        public int UserId { get; set; }
        public DateOnly CreatedAt { get; set; }

        public int UserRoleId { get; set; }

    }
}
