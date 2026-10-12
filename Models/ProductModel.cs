using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ProductModel
    {
# tell about the properties 
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
