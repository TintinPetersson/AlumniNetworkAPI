using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Posts;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Events
{
    public class EventCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool AllowGuests { get; set; }
        public string? BannerImage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? GroupId {get; set; }
    }
}
