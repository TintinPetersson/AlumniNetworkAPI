namespace AlumniNetworkAPI.CustomExceptions
{
    public class TopicNotFoundException : Exception
    {
        public TopicNotFoundException(int id) : base($"Topic with id: {id} was not found")
        {
        }
    }
}