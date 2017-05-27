using System.Web.Http;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

namespace Administration.Controllers
{
    [RoutePrefix("api/account")]
    public class ApiAccountController : ApiController
    {
        #region Properties

        /// <summary>
        /// Contains functions & repositories to access into database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate controller with injectors
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ApiAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Hello world method.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var accounts = _unitOfWork.RepositoryAccount.Search();
            return Json(accounts);
        }

        #endregion
    }
}