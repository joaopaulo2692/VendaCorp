using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;

namespace VendaCorp.Infrastructure.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserRepository(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> CreateUser(ApplicationUser user)
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

        public Task<List<ApplicationUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<Result> RemoveUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
