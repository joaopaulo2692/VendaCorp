using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Profiles
{
    public class EnterpriseProfile : Profile
    {
        public EnterpriseProfile()
        {
            CreateMap<Enterprise, EnterpriseVO>()
                .ReverseMap();

            CreateMap<EnterpriseVO, Enterprise>()
                .ReverseMap();

            CreateMap<EnterpriseCreateVO, Enterprise>()
               .ReverseMap();
        }
    }
}
