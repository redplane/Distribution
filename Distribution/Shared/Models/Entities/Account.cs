using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.Entities
{
    [Table(nameof(Account))]
    public class Account
    {
        #region Properties

        /// <summary>
        /// Id of account.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Email which is used for registering email.
        /// </summary>
        public string Email { get; set; }

        #endregion
    }
}