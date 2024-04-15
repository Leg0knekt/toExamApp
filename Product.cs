using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toExamApp
{
    public class Product
    {
        public Product(int id, string name, int amount, string unit, int cost)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Unit = unit;
            Cost = cost;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public int Cost { get; set; }
    }
}
