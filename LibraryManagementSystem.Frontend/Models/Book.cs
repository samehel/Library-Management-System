﻿using System.Collections.Generic;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string DeweyDecimalNumber { get; set; }
        public List<CartBook> CartBooks { get; set; }
    }
}
