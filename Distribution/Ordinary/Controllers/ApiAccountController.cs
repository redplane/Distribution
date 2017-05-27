using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Shared.Interfaces.Repositories;
using Shared.Models.Entities;
using Shared.ViewModels;

namespace Ordinary.Controllers
{
    [RoutePrefix("api/account")]
    public class ApiAccountController : ApiController
    {
        #region Properties

        /// <summary>
        /// Unit of work 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate account controller,
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ApiAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Get accounts list.
        /// </summary>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] RegisterAccountViewModel conditions)
        {
            if (conditions == null)
            {
                conditions = new RegisterAccountViewModel();
                Validate(conditions);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find accounts from database.
            var accounts = _unitOfWork.RepositoryAccount.Search();
            accounts = accounts.Where(x => x.Email.Equals(conditions.Email, StringComparison.InvariantCultureIgnoreCase));

            // Find the first matched account.
            var account = await accounts.FirstOrDefaultAsync();
            if (account != null)
                return Conflict();
            
            // Initiate account.
            account = new Account();
            account.Email = conditions.Email;

            // Save changes.
            _unitOfWork.RepositoryAccount.Insert(account);
            await _unitOfWork.CommitAsync();

            return Ok(account);
        }

        #endregion
    }
}