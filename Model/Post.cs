namespace BachelorOppgaveBackend.Model
{
    public class Post
    {
        public Guid id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int user_id { get; set; }

        public int category_id { get; set; }

        public DateTime created_at { get; set; }


    }
}
