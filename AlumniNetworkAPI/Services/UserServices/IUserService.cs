using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string keycloakId, string username);
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<User> PostAsync(string keycloakId, string username);
        Task<bool> UserInDb(string keycloakId);
    }
}
