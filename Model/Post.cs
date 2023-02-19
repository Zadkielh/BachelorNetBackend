namespace BachelorOppgaveBackend.Model
{
    public class Post
    {
        public int post_id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int user_id { get; set; }

        public int category_id { get; set; }

        public DateOnly created_at { get; set; }


    }
}
