using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Groups
{
    public class GroupEditDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }

    }
}
