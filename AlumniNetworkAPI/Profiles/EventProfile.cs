using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventReadDto>();

            CreateMap<Event, EventGroupReadDto>()
            .ForMember(cdto => cdto.UsersResponded, opt => opt
            .MapFrom(c => c.AcceptedUsers.Select(c => c.Id).ToArray()));

            CreateMap<Event, EventUserReadDto>();

            CreateMap<EventCreateDto, Event>();

            CreateMap<EventEditDto, Event>();
        }
    }
}