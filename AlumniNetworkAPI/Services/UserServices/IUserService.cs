using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string keycloakId);
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
    }
}
