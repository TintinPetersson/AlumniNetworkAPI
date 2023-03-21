using AlumniNetworkAPI.Models.Domain;
using System.Collections.Generic;

namespace AlumniNetworkAPI.Services.TopicServices
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetTopicsAsync(string? search, int? limit, int? offset);
        Task<IEnumerable<Topic>> GetTopicsByIdAsync(int id);
        Task<Topic> AddTopicAsync(Topic newTopic, string keycloakId);
        Task AddUserToTopicAsync(int topicId, string keycloakId);
    }
}
