namespace BachelorOppgaveBackend.Model
{
    public class Vote
    {
        public Guid id { get; set; }

        public int user_id { get; set; }

        public int post_id { get; set; }

        public DateTime created_at { get; set; }
    }
}
