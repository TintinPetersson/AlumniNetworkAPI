using AlumniNetworkAPI.Models.Dtos.Users;

namespace AlumniNetworkAPI.Models.Dtos.Events
{
    public class EventGroupReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } //Nullable
        public DateTime LastUpdated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllowGuests { get; set; }
        public List<int>? UsersResponded { get; set; } //Many-Many
        public UserPostReadDto? Author { get; set; }
    }
}
