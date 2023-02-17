namespace BachelorOppgaveBackend.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public int ParentId { get; set; }

        public int UserId { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
