﻿using System.ComponentModel.DataAnnotations;

namespace Marketteto.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
