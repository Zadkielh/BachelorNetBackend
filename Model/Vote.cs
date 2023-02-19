namespace BachelorOppgaveBackend.Model
{
    public class Vote
    {
        public int vote_id { get; set; }

        public int user_id { get; set; }

        public int post_id { get; set; }

        public DateOnly created_at { get; set; }
    }
}
