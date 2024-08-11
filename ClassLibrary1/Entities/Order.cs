using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class Order : BaseEntities
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public string OrderCode { get; set; }
        public string OrderDate { get; set; }
        public string OrderItems { get; set; }
        public string Products { get; set; }
        public string TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public string Status { get; set; }

        public string EnterpriseId { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}
