using System.ComponentModel.DataAnnotations;

namespace MOP.Abstract.Dto
{
    public class UserCredentials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
