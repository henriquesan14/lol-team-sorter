namespace LoLTeamSorter.Application.Exceptions
{
    public class InvalidCurrentPasswordException : BadRequestException
    {
        public InvalidCurrentPasswordException(string message) : base(message)
        {
        }
    }
}
