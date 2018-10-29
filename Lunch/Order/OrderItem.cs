using System;
using System.ComponentModel.DataAnnotations;
using Lunch.Data;

namespace Lunch.Order
{
    public class OrderItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Person { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal Total => Amount * Price;
        public string Note { get; set; }

        public OrderItem() {}

        public OrderItem(string description, string person)
        {
            Description = description;
            Person = person;
        }
    }
}