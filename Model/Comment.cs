namespace BachelorOppgaveBackend.Model
{
    public class Comment
    {
        public Guid id { get; set; }
        public Guid post_id { get; set; }

        public Guid comment_id { get; set; }

        public Guid user_id { get; set; }
        public string? content { get; set; }

        public DateTime created_at { get; set; }
    }
}
