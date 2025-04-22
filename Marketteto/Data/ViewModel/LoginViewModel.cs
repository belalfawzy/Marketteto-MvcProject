using System.ComponentModel.DataAnnotations;

namespace Marketteto.Data.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
