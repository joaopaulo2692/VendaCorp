using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Infrastructure.Data;

namespace VendaCorp.Infrastructure.Repository
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly ApplicationDbContext _db;

        public EnterpriseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result> ActivateAsync(Enterprise enterprise)
        {
            Enterprise enterpriseDb = await _db.Enterprises.Where(x => x.Id == enterprise.Id).FirstOrDefaultAsync();
            if (enterpriseDb == null) return Result.Fail("Empresa não localizada");

            enterprise.UpdatedAt = DateTime.Now;
            enterprise.DisabledAt = null;
            enterpriseDb.Activate = true;
            enterpriseDb.Status = ConstantsEnterprise.Activate;
            await _db.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> CreateAsync(Enterprise enterprise)
        {
            enterprise.UpdatedAt = DateTime.Now;
            enterprise.CreatedAt = DateTime.Now;
            enterprise.Activate = false;
            enterprise.Status = ConstantsEnterprise.Waiting;

            _db.Enterprises.Add(enterprise);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DisableAsync(Enterprise enterprise)
        {
            Enterprise enterpriseDb = await _db.Enterprises.Where(x => x.Id == enterprise.Id).FirstOrDefaultAsync();
            if (enterpriseDb == null) return Result.Fail("Empresa não localizada");

            enterprise.DisabledAt = DateTime.Now;
            enterprise.UpdatedAt = DateTime.Now;
            enterpriseDb.Activate = false;
            enterpriseDb.Status = ConstantsEnterprise.Disable;
            _db.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Enterprise> GetById(int id)
        {
            Enterprise enterpriseDb = await _db.Enterprises.Where(x => x.Id == id).FirstOrDefaultAsync();

            return enterpriseDb;
        }

        public async Task<Enterprise> GetByLegalName(string legalName)
        {
            Enterprise enterpriseDb = await _db.Enterprises.Where(x => x.LegalName == legalName).FirstOrDefaultAsync();

            return enterpriseDb;
        }

        public async Task<Enterprise> GetByTradeName(string tradeName)
        {
            Enterprise enterpriseDb = await _db.Enterprises.Where(x => x.TradeName == tradeName).FirstOrDefaultAsync();

            return enterpriseDb;
        }
    }
}
