using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class Order : BaseEntities
    {
        public string Id { get; set; }
        public int DeliveryOrderId { get; set; }
        //public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderItems { get; set; }
        public string Products { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public string Status { get; set; }

        public int EnterpriseId { get; set; }

        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual Enterprise Enterprise { get; set; }
        public Order()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000000, 1000000000);
            Id = "RO" + randomNumber.ToString();
        }


    }
  
}
