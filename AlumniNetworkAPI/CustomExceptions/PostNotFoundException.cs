namespace AlumniNetworkAPI.CustomExceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(int id) : base($"Post: {id} was not found")
        {
        }
    }
}
