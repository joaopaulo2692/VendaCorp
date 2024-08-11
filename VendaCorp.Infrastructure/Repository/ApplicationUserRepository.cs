using FluentResults;
using Microsoft.AspNetCore.Identity;
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
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<Result> CreateUserAsync(ApplicationUser user)
        {
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.Name = user.Name;
                user.Email = user.Email;
                user.EmailConfirmed= true;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                user.DisabledAt = null;

                IdentityResult resultCreateUser = await _signInManager
                                                            .UserManager
                                                            .CreateAsync(user, user.PasswordHash);

                if (!resultCreateUser.Succeeded)
                    return Result.Fail(resultCreateUser.Errors.FirstOrDefault().Description);

                return Result.Ok().WithSuccess(user.Id);
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            List<ApplicationUser> users = await _db.Users.ToListAsync();

            return users;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            ApplicationUser user = await _db.Users.Where(x => x.Email== email && x.DisabledAt == null).FirstOrDefaultAsync();
            return user;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            ApplicationUser user = await _db.Users.Where(x => x.Id == id && x.DisabledAt == null).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Result> RemoveAsync(ApplicationUser user)
        {
            ApplicationUser userDb = await _db.Users.Where(x => x.Id == user.Id && x.DisabledAt == null).FirstOrDefaultAsync();

            userDb.UpdatedAt = DateTime.Now;
            userDb.DisabledAt = DateTime.Now;

            await _db.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
