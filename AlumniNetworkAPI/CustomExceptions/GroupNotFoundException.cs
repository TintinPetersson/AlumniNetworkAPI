namespace AlumniNetworkAPI.CustomExceptions
{
    public class GroupNotFoundException: Exception
    {
        public GroupNotFoundException(int id) : base($"Group: {id} was not found")
        {
        }
    }
}
