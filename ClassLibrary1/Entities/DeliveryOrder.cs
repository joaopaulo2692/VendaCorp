using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    [Table("DeliveryOrder")]
    public class DeliveryOrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ShippningCompanyName { get; set; }
        public string CustomerAddress { get; set; }       
        public string Status { get; set; }
        public int ShippningCompanyId { get; set; }
     

        public virtual Order Order { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }

     
    }

    
}
