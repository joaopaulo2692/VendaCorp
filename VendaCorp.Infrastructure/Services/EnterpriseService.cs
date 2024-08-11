using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepo;
        private readonly IMapper _mapper;

        public EnterpriseService(IEnterpriseRepository enterpriseRepo, IMapper mapper)
        {
            _enterpriseRepo = enterpriseRepo;
            _mapper = mapper;
        }

        public async Task<Result> ActivateAsync(int id)
        {
            Enterprise enterprise = await _enterpriseRepo.GetById(id);
            if (enterprise == null) return Result.Fail("Empresa não localizada");

            Result response = await _enterpriseRepo.ActivateAsync(enterprise);
            return response;
        }

        public async Task<Result> CreateAsync(EnterpriseVO enterpriseVO)
        {
            Enterprise enterprise = _mapper.Map<Enterprise>(enterpriseVO);
            Result response = await  _enterpriseRepo.CreateAsync(enterprise);

            return response;
        }

        public async Task<Result> DisableAsync(int id)
        {
            Enterprise enterprise = await _enterpriseRepo.GetById(id);
            if (enterprise == null) return Result.Fail("Empresa não localizada");

            Result response = await _enterpriseRepo.DisableAsync(enterprise);
            return response;
        }

        public async Task<EnterpriseVO> GetById(int id)
        {
            Enterprise enterprise = await _enterpriseRepo.GetById(id);
            if (enterprise == null) return new EnterpriseVO();

            EnterpriseVO enterpriseVO = _mapper.Map<EnterpriseVO>(enterprise);

            return enterpriseVO;
        }

        public async Task<EnterpriseVO> GetByLegalName(string legalName)
        {
            Enterprise enterprise = await _enterpriseRepo.GetByLegalName(legalName);
            if (enterprise == null) return new EnterpriseVO();

            EnterpriseVO enterpriseVO = _mapper.Map<EnterpriseVO>(enterprise);

            return enterpriseVO;
        }

        public async Task<EnterpriseVO> GetByTradeName(string tradeName)
        {
            Enterprise enterprise = await _enterpriseRepo.GetByLegalName(tradeName);
            if (enterprise == null) return new EnterpriseVO();

            EnterpriseVO enterpriseVO = _mapper.Map<EnterpriseVO>(enterprise);

            return enterpriseVO;
        }
    }
}
