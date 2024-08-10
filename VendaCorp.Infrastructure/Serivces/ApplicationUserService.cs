using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.ApplicationUser;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Serivces
{
    
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationRepo;

        public ApplicationUserService(IApplicationUserRepository applicationRepo)
        {
            _applicationRepo = applicationRepo;
        }

        public Task<Result> CreateAsync(UserVO user)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteAsync(string idUser)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserVO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserVO> GetByIdAsync(string idUser)
        {
            throw new NotImplementedException();
        }
    }
}
