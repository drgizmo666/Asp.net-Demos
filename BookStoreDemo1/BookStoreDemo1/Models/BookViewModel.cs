using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreDemo1.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(160)]
        public string Author { get; set; }
        [Required]
        [Range(typeof(decimal), "0.00", "49.99")]
        public decimal Price {get; set; }
        [Required]
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string ISBN { get; set; }
        //public Stack StackItem { get; set; }
        public string Location { get; set; }
    }
}