namespace BachelorOppgaveBackend.Model
{
    public class Vote
    {
        public Guid id { get; set; }

        public Guid user_id { get; set; }

        public Guid post_id { get; set; }

        public DateTime created_at { get; set; }
    }
}
