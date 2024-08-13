using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        [NotMapped]
        public double Subtotal => Quantity * Price;

        public virtual Order? Order { get; set; }
    }
}
