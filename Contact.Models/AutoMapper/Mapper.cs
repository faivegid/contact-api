using AutoMapper;
using Contact.Models;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DTO.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserContact, UserDTO>().ReverseMap();
            CreateMap<UserContact, UpdateUserDTO>().ReverseMap();
            CreateMap<UserContact, RegisterDTO>().ReverseMap();
        }
    }
}
