using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface IShippingCompanyRepository
    {
        public Task<ShippingCompany> GetByName(string name);
    }
}
