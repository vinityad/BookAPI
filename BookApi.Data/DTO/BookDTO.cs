using System;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Data.Model
{
    public class BookDTO
    {
        public string BookId { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string[] Authors { get; set; }
    }
}
