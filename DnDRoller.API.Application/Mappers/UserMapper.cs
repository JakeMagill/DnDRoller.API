using System;
using AutoMapper;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Application.DTOs;

namespace DnDRoller.API.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}