namespace BachelorOppgaveBackend.Model
{
    public class Post
    {
        public Guid id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public Guid user_id { get; set; }

        public Guid category_id { get; set; }

        public DateTime created_at { get; set; }


    }
}
