namespace AlumniNetworkAPI.Models.Dtos.Users
{
    public class UserPostReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public string? Bio { get; set; } //Nullable
        public string? FunFact { get; set; } //Nullable
        public string? Picture { get; set; }
    }
}
