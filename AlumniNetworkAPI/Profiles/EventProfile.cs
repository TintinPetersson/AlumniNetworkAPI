using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class EventProfile :Profile
    {
        public EventProfile()
        {
            CreateMap<EventCreateDto, Event>();
            CreateMap<EventEditDto, Event>();
            CreateMap<Event, EventReadDto>();
        }
    }
}
