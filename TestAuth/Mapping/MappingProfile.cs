using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Models;

namespace TestAuth.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ComputerPartsModel, ComputerParts>();
            CreateMap<ComputerParts, ComputerPartsModel>();
            CreateMap<RegisterModel, User>()
                .ForMember(dest =>
                dest.UserName,
                opt => opt.MapFrom(src => src.Email));
            CreateMap<LoginModel, User>();
            CreateMap<User, ConfirmEmailModel>()
                .ForMember(dest =>
                dest.userId,
                opt=>opt.MapFrom(src=>src.Id));
        }
    }
}
