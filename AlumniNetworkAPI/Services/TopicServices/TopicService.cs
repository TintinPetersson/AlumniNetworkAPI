using AlumniNetworkAPI.CustomExceptions;
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

    #region READ
    public async Task<IEnumerable<Topic>> GetTopicsAsync(string? search, int? limit, int? offset)
    {
        IQueryable<Topic> query = _context.Topics;

        if (!string.IsNullOrEmpty(search))
            query = query.Where(t => t.Name.Contains(search));     

        if (offset.HasValue)
            query = query.Skip(offset.Value);    
       
        if (limit.HasValue)
            query = query.Take(limit.Value);

        return await query.ToListAsync();
    }
    public async Task<IEnumerable<Topic>> GetTopicsByIdAsync(int id)
    {
        var topics = await _context.Topics.Where(t => t.Id == id).ToListAsync();
        if (topics.Count == 0)
        {
            throw new TopicNotFoundException(id);
        }
        return topics;
    }
    #endregion
    #region CREATE
    public async Task<Topic> AddTopicAsync(Topic newTopic, string keycloakId)
    {
        User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
        var users = new List<User>
        {
            user
        };
        newTopic.Users = users;
        await _context.Topics.AddAsync(newTopic);
        await _context.SaveChangesAsync();
        return newTopic;

    }
    #endregion
    #region UPDATE
    public async Task AddUserToTopicAsync(int topicId, string keycloakId)
    {
        var topic = await _context.Topics.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == topicId);
        if (topic == null)
        {
            throw new TopicNotFoundException(topicId);
        }

 
        var user = await _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        if (topic.Users.Contains(user))
        {
            throw new ArgumentException("User is already a member of this topic.");
        }
     
        topic.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    #endregion

}


