﻿namespace DomainCentricDemo.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Author>  Authors { get; set; } = new();
    }
}
