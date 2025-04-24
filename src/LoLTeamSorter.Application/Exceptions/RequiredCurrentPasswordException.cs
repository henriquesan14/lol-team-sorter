namespace LoLTeamSorter.Application.Exceptions
{
    public class RequiredCurrentPasswordException : BadRequestException
    {
        public RequiredCurrentPasswordException(string message) : base(message)
        {
        }
    }
}
