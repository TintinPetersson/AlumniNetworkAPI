using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Topics
{
    public class TopicCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
