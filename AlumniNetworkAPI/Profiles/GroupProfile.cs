﻿using AlumniNetworkAPI.Dtos;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Models.Dtos.Users;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class GroupProfile :Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupEditDto, Group>();
            CreateMap<Group, GroupReadDto>();
        }
    }
}
