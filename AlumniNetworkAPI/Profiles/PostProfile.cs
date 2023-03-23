using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateDto, Post>();
            CreateMap<PostEditDto, Post>();
            CreateMap<PostReplyCreateDto, Post>();
            CreateMap<Post, PostReadDto>();
            CreateMap<Post, PostGroupReadDTO>();
        }
    }
}
