using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly AlumniNetworkDbContext _context;

        public EventService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(string keycloakId)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            return await _context.Events
                .Where(e => e.AcceptedUsers.Any(u => u.Id == user.Id))
                .ToListAsync();
        }
      
        public async Task<Event> AddEventAsync(Event newEvent, string keycloakId)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            var users = new List<User>
            {
                user
            };

            newEvent.LastUpdated= DateTime.UtcNow;
            newEvent.AcceptedUsers = users;
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

    }
}
