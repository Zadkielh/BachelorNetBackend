﻿namespace BachelorOppgaveBackend.Model
{
    public class Category
    {
        public int category_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public DateOnly created_at { get; set; }
    }
}
