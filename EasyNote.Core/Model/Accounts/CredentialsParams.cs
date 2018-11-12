using System.ComponentModel.DataAnnotations;

namespace EasyNote.Core.Model.Accounts
{
    public class CredentialsParams
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
