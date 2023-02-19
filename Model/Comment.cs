namespace BachelorOppgaveBackend.Model
{
    public class Comment
    {
        public int comment_id { get; set; }
        public int post_id { get; set; }

        public int parent_id { get; set; }

        public int user_id { get; set; }
        public string? content { get; set; }

        public DateOnly created_at { get; set; }
    }
}
