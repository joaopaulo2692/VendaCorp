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
        public int Id { get; set; }
        public string DeliveryOrderId { get; set; }
        //public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderItems { get; set; }
        public string Products { get; set; }
        public string TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public string Status { get; set; }

        public string EnterpriseId { get; set; }

        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}
