namespace LoLTeamSorter.Application.Exceptions
{
    public class UserAlreadyExistsException : ConflictException
    {
        public UserAlreadyExistsException(string username) : base("User", username)
        {
        }
    }
}
