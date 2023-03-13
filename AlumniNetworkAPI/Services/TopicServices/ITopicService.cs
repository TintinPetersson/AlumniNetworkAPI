using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.TopicServices
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetTopicsAsync(string keycloakId);
        Task<IEnumerable<Topic>> GetTopicsByIdAsync(int id);
    }
}
