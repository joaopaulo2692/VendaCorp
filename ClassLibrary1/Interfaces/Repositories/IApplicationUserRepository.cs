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
        
        public Task<Result> CreateUser(ApplicationUser user);
        public Task<Result> RemoveUser(ApplicationUser user);
        public Task<List<ApplicationUser>> GetAllUsers();
    }
}
