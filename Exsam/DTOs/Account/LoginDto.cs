using System.ComponentModel.DataAnnotations;

namespace Exsam.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
