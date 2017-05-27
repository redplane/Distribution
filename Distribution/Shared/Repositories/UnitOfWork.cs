using System.Data.Entity;
using System.Threading.Tasks;
using Shared.Interfaces.Repositories;

namespace Shared.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructor

        /// <summary>
        ///     Initiate unit of work with database context.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Context which is used for accessing into database.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Repository which provides function to access into account database.
        /// </summary>
        private IRepositoryAccount _repositoryAccount;

        /// <summary>
        ///     Repository which provides function to access into account database.
        /// </summary>
        public IRepositoryAccount RepositoryAccount
            => _repositoryAccount ?? (_repositoryAccount = new RepositoryAccount(_dbContext));

        #endregion

        #region Methods

        /// <summary>
        /// Commit changes to database.
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Commit changes to database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}