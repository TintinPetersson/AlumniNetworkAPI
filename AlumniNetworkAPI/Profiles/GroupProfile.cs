using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupEditDto, Group>();
            CreateMap<Group, GroupReadDto>();
        }
    }
}
