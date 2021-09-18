using AutoMapper;
using Contact.Models;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DTO.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Contact, UserDTO>().ReverseMap();
            CreateMap<Contact, UpdateUserDTO>().ReverseMap();
            CreateMap<Contact, RegisterDTO>().ReverseMap();
        }
    }
}
