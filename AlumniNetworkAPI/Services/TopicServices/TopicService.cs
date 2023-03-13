using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.TopicServices;

public class TopicService : ITopicService
{
    private readonly AlumniNetworkDbContext _context;

    public TopicService(AlumniNetworkDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Topic>> GetTopicsAsync(string keycloakId)
    {
        User user = _context.Users.First(u => u.KeycloakId == keycloakId);

        return await _context.Topics
            .Include(t => t.Events)
            .Include(t => t.Users)
            .Include(t => t.Posts)
            .Where(t => t.Events.Any(e => e.InvitedUsers.Contains(user)) || t.Posts.Any(p => p.AuthorId == user.Id))
            .ToListAsync();
    }
    public async Task<IEnumerable<Topic>> GetTopicsByIdAsync(int id)
    {
        return await _context.Topics.Where(t => t.Id == id).ToListAsync();
    }
}


