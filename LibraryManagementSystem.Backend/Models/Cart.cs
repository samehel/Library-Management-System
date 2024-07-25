﻿namespace LibraryManagementSystem.Backend.Models
{
    public class Cart
    {
        public Guid ID { get; set; }
        public int UserID { get; set; }
        public List<CartBook>? CartBooks { get; set; }
    }
}
