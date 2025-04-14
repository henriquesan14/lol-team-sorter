using LoLTeamSorter.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly LoLTeamSorterDbContext _dbContext;

        public UnitOfWork(LoLTeamSorterDbContext dbContext, IPlayerRepository players, ITeamRepository teams, IMatchmakingRepository matchmakings)
        {
            _dbContext = dbContext;
            Players = players;
            Teams = teams;
            Matchmakings = matchmakings;
        }

        public IPlayerRepository Players { get; }
        public ITeamRepository Teams { get; }
        public IMatchmakingRepository Matchmakings { get; }

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            IsDisposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void IsDisposing(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
