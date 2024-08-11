using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Application.DTO.Order
{
    public class OrderCreateVO
    {
        public string OrderItems { get; set; }
        public List<string> Products { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }
        public string EnterpriseId { get; set; }
    }
}
