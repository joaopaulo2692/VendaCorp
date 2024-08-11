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
        public Task<Result> CreateAsync(EnterpriseVO enterprise);
        public Task<Result> ActivateAsync(int id);
        public Task<Result> DisableAsync(int id);
    }
}
