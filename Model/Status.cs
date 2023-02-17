namespace BachelorOppgaveBackend.Model
{
    public class Status
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; }

        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
