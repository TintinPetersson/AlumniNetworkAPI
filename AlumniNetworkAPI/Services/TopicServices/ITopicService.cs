using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.TopicServices
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<IEnumerable<Topic>> GetTopicsByIdAsync(int id);
        Task<Topic> AddTopicAsync(Topic newTopic, string keycloakId);
        Task AddUserToTopicAsync(int topicId, string keycloakId);
    }
}
