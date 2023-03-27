namespace AlumniNetworkAPI.Models.Dtos.Groups
{
    public class GroupUserReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Body { get; set; }
        public bool IsPrivate { get; set; }
    }
}
