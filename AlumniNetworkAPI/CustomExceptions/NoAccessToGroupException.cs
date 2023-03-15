namespace AlumniNetworkAPI.CustomExceptions
{
    public class NoAccessToGroupException : Exception
    {
        public NoAccessToGroupException(int userId, int groupId) : base($"User with Id:{userId} has no access to group with Id: {groupId}")
        {
        }


    }
}
