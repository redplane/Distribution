using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Ordinary.Constants;
using Ordinary.Interfaces;
using Shared.Interfaces.Repositories;
using Shared.Models.Entities;
using Shared.Models.Messages;
using Shared.ViewModels;
using Shared.Services;

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

        /// <summary>
        /// Service which is for handling queue.
        /// </summary>
        private readonly IMqService _mqService;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initiate account controller,
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mqService"></param>
        public ApiAccountController(IUnitOfWork unitOfWork, IMqService mqService)
        {
            _unitOfWork = unitOfWork;
            _mqService = mqService;
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

            // Broadcast notification to hub.
            var accountRegistrationMessage = new AccountRegistrationMessage();
            accountRegistrationMessage.Email = conditions.Email;
            accountRegistrationMessage.Time = 0;
            _mqService.Send(MqNames.AccountRegistration, accountRegistrationMessage);

            return Ok(account);
        }

        #endregion
    }
}