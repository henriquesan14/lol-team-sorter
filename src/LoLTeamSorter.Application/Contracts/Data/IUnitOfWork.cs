namespace LoLTeamSorter.Application.Contracts.Data
{
    public interface IUnitOfWork
    {
        IPlayerRepository Players { get; }
        ITeamRepository Teams { get; }
        IMatchmakingRepository Matchmakings { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}
