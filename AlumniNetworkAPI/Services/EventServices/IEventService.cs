using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.EventServices
{
    public interface IEventService
    { 
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> AddEventAsync(Event newEvent, string keycloakId);
        Task UpdateEventAsync(Event newEvent, string keycloakId, int id);
        Task CreateGroupEventInvitation(int eventId, int groupId);
        Task DeleteGroupEventInvitation(int eventId, int groupId);
        Task CreateTopicEventInvitation(int eventId, int topicId);
        Task DeleteTopicEventInvitation(int eventId, int topicId);
        Task CreateUserEventInvitation(int eventId, int userId);    
        Task DeleteUserEventInvitation(int eventId, int userId);
        Task CreateEventRSVP(int eventId, string keycloakId);
        public bool Exists(int id);
    }
}
