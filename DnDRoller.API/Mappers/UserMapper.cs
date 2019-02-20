using System;
using AutoMapper;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Domain.DTOs;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}