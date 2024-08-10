using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.ApplicationUser;

namespace VendaCorp.Core.Interfaces.Services
{
    public interface IApplicationUserService
    {
        public Task<Result> CreateAsync(UserVO user);
        public Task<Result> DeleteAsync(string idUser);
        public Task<UserVO> GetByIdAsync(string idUser);
        public Task<List<UserVO>> GetAllAsync();
    }
}
