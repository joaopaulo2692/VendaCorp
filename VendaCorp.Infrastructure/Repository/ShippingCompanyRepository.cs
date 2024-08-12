using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Infrastructure.Data;

namespace VendaCorp.Infrastructure.Repository
{
    public class ShippingCompanyRepository : IShippingCompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public async Task<ShippingCompany> GetByName(string name)
        {
            ShippingCompany shippingCompany = await _db.ShippingCompanies.Where(x => x.Name == name).FirstOrDefaultAsync();

            return shippingCompany;
        }
    }
}
