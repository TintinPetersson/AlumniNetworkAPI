using AlumniNetworkAPI.Dtos;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Users;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();

            CreateMap<User, UserPostReadDto>();

            CreateMap<UserCreateDto, User>();

            CreateMap<UserEditDto, User>();
        }
    }
}
