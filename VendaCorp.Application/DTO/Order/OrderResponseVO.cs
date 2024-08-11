using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Application.DTO.Order
{
    public class OrderResponseVO
    {
        public string Id { get; set; }
        public int DeliveryOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderItems { get; set; }
        public string Products { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public string Status { get; set; }

        public string EnterpriseId { get; set; }
    }
}
