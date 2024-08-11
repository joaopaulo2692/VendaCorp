using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class SalesOrder
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string CarrierName { get; set; }
        public string CustomerAddress { get; set; }       
        public string Status { get; set; }
        public bool IsAproved { get; set; }
        public int ShippningCompanyId { get; set; }
     

        public virtual Order Order { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }

        public SalesOrder()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000000, 1000000000);
            Id = "RO"+randomNumber.ToString();
        }
    }

    
}
