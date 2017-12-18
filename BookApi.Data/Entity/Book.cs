using System;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Data.Entity
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class Book : EntityBase
    {
        [StringLength(40)]
        public string BookId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime DateOfPublication { get; set; }

        [StringLength(500)]
        public string Authors { get; set; }
    }
}
