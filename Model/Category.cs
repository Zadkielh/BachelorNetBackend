namespace BachelorOppgaveBackend.Model
{
    public class Category
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public DateTime created_at { get; set; }
    }
}
