﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.Entities
{
    public class Enterprise : BaseEntities
    {
        public int Id { get; set; }
        public string TradeName { get; set; }
        public string LegalName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public bool Activate { get; set; } = false;
        public string Document { get; set; }

        public List<Order> Orders { get; set; }
    }
}
