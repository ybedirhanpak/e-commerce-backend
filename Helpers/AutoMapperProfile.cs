using System;
using AutoMapper;
using e_commerce_api.Dtos;
using e_commerce_api.Models;

namespace e_commerce_api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
