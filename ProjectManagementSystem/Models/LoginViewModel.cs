using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class LoginViewModel
    {
        public int id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Users Name")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string PersonalName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string designationName { get; set; }
    }
}
