using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Topics;
using AlumniNetworkAPI.Models.Dtos.Users;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicCreateDto, Topic>();
            CreateMap<TopicEditDto, Topic>();
            CreateMap<Topic, TopicReadDto>();
        }
    }
}
