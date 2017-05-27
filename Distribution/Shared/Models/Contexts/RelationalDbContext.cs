using System.Data.Entity;
using Shared.Models.Entities;

namespace Shared.Models.Contexts
{
    public class RelationalDbContext : DbContext
    {
        #region Properties

        /// <summary>
        /// List of account.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate database context with connection obtained from App.config.
        /// </summary>
        public RelationalDbContext() : base("iDistribution")
        {
            
        }

        #endregion
    }
}