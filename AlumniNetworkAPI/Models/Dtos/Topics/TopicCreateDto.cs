using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Topics
{
    public class TopicCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
