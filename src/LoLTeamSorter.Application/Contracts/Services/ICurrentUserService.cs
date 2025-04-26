namespace LoLTeamSorter.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? IpAddress { get; }
    }
}
