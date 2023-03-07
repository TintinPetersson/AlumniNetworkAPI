using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Events
{
    public class EventEditDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool AllowGuests { get; set; }
        public string? BannerImage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
