using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AlumniNetworkDbContext _context;
        public UserService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string keycloakId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.KeycloakId == keycloakId);

            if(user == null)
            {
                throw new Exception($"User with keycloakId: {keycloakId} not found");
            }
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if(user == null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (!await UserExists(user.Id))
            {
                throw new UserNotFoundException(user.Id);
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(c => c.Id == id);
        }
    }
}