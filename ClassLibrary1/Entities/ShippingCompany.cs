using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class ShippingCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DeliveryOrder> DeliveryOrder { get; set; }
    }
}
