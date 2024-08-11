using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface IApplicationUserRepository
    {
        
        public Task<Result> CreateUserAsync(ApplicationUser user);
        public Task<Result> RemoveAsync(ApplicationUser user);
        public Task<List<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByEmailAsync(string id);
        public Task<ApplicationUser> GetByIdAsync(string id);
        public Task<Result> SignInUser(ApplicationUser user, string password);
    }
}
