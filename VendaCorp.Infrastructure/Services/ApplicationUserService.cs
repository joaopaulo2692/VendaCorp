using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.ApplicationUser;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Serivces
{
    
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationRepo;
        private readonly IMapper _mapper;

        public ApplicationUserService(IApplicationUserRepository applicationRepo, IMapper mapper)
        {
            _applicationRepo = applicationRepo;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(UserCreateVO user)
        {
            ApplicationUser userDb = await _applicationRepo.GetByEmailAsync(user.Email);
            if (userDb != null) return Result.Fail("Usuário com e-mail já cadastrado");

            ApplicationUser userToSave = _mapper.Map<ApplicationUser>(user);
            Result response = await _applicationRepo.CreateUserAsync(userToSave);
            return response;
        }

        public async Task<Result> DeleteAsync(string idUser)
        {
            ApplicationUser userDb = await _applicationRepo.GetByIdAsync(idUser);
            if (userDb != null) return Result.Fail("Usuário não cadastrado");
            Result response = await _applicationRepo.RemoveAsync(userDb);
            return response;

        }

        public async Task<List<UserVO>> GetAllAsync()
        {
            List<ApplicationUser> users = await _applicationRepo.GetAllAsync();

            List<UserVO> usersVO = _mapper.Map<List<UserVO>>(users);

            return usersVO;
        }

        public async Task<UserVO> GetByIdAsync(string idUser)
        {
            ApplicationUser user = await _applicationRepo.GetByIdAsync(idUser);
            UserVO userVO = _mapper.Map<UserVO>(user);
            return userVO;
        }

        public async Task<Result> Login(LoginVO user)
        {
            try
            {
                ApplicationUser client = await _applicationRepo.GetByEmailAsync(user.Email);
                if (client == null)
                {
                    return Result.Fail("Erro ao fazer Login");
                }
                Result response = await _applicationRepo.SignInUser(client, user.Password);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Fail("Erro ao fazer Login");
            }
        }
    }
}
