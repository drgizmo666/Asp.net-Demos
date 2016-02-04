﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreDemo1.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price {get; set; }
        public string ISBN { get; set; }
        //public Stack StackItem { get; set; }
        public string Location { get; set; }
    }
}