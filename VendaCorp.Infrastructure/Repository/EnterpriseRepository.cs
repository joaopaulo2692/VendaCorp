using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            enterpriseDb.Activate = true;
            enterpriseDb.Status = ConstantsEnterprise.Activate;
            _db.SaveChangesAsync();

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
    }
}
