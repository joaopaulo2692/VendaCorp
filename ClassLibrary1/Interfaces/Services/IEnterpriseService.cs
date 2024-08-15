using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Services
{
    public interface IEnterpriseService
    {
        public Task<Result> CreateAsync(EnterpriseCreateVO enterprise);
        public Task<Result> ActivateAsync(int id);
        public Task<Result> DisableAsync(int id);

        public Task<EnterpriseVO> GetById(int id);
        public Task<List<EnterpriseVO>> GetAll();

        public Task<EnterpriseVO> GetByTradeName(string tradeName);
        public Task<EnterpriseVO> GetByLegalName(string LegalName);
    }
}
