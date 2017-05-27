using System.Data.Entity;
using Shared.Interfaces.Repositories;
using Shared.Models.Entities;

namespace Shared.Repositories
{
    public class RepositoryAccount: ParentRepository<Account>, IRepositoryAccount
    {
        #region Constructor

        /// <summary>
        /// Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryAccount(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}