using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels
{
    public class RegisterAccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}