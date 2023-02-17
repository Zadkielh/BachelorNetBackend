namespace BachelorOppgaveBackend.Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public DateOnly CreatedAt { get; set; }


    }
}
