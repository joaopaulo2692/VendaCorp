using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Application.DTO.Enterprise
{
    public class EnterpriseCreateVO
    {
        public string TradeName { get; set; }
        public string LegalName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
    }
}
