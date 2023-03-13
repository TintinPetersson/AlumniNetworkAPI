namespace AlumniNetworkAPI.Models.Dtos.Users
{
    public class UserEditDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Status { get; set; }
        public string? Bio { get; set; }
        public string? FunFact { get; set; }
        public string? Picture { get; set; }
    }
}
