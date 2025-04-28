namespace LoLTeamSorter.Application.Exceptions
{
    public class InvalidWinningTeamException : BadRequestException
    {
        public InvalidWinningTeamException(string message) : base(message)
        {
        }
    }
}
