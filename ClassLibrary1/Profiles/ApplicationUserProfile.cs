using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.ApplicationUser;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, UserVO>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserCreateVO>()
               .ReverseMap();

        }
    }
}
