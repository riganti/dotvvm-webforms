using System;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.WebForms.Tutorial.Model
{
    public class ProductDTO
    {
        public bool IsSelected { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public double UnitsInStock { get; set; }
    }
}