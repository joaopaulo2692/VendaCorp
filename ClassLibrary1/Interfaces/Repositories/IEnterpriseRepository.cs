using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface IEnterpriseRepository
    {
        public Task<Result> CreateAsync(Enterprise enterprise);
        public Task<Result> ActivateAsync(Enterprise enterprise);
        public Task<Result> DisableAsync(Enterprise enterprise);
        public Task<Enterprise> GetById(int id);

        public Task<Enterprise> GetByTradeName(string tradeName);
        public Task<Enterprise> GetByLegalName(string LegalName);
     }
}
