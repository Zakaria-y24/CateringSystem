using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This represents a single catering item in your system
    /// </summary>
    public class CateringItem
    {
        public CateringItem()
        {

        }
        public CateringItem(string type, string code, string name, decimal price)
        {
            this.Type = type;
            this.Code = code;
            this.Name = name;
            this.Price = price;
        }

        public string Type { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; } = 25;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string IsSoldOut()
        {
            if (Quantity > 0)
            {
                return Quantity + " remaining";
            } 
            return "SOLD OUT";
        }

        public void PurchaseProduct(int quantity)
        {
            Quantity -= quantity;
        }

        public string ItemTypeName()
        {
            if (this.Type == "B")
            {
                return "Beverage";
            }
            else if (this.Type == "E")
            {
                 return "Entree";
            }
            else if (this.Type == "A")
            {
                return "Appetizer";
            }
            else
            {
                return "Dessert";
            }  
        }
    }
}
