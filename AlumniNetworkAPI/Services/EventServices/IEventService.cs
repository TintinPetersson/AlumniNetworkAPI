using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.EventServices
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync(string keycloakId);
        Task<Event> AddEventAsync(Event newEvent, string keycloakId);
    }
}
