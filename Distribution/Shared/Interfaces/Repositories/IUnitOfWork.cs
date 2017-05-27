using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        #region Properties

        /// <summary>
        /// Repository which provides functions to access account database.
        /// </summary>
        IRepositoryAccount RepositoryAccount { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Save change to database synchronously.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// Save change to database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        #endregion
    }
}