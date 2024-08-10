using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO;

namespace VendaCorp.Core.Interfaces.Services
{
    public interface IProductExternApiService
    {
        public Task<List<ProductVO>> GetAll();
    }
}
