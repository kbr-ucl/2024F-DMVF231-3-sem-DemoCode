﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Model
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
