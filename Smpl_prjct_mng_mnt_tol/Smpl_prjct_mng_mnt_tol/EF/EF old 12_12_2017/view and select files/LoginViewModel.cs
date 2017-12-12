using System.ComponentModel.DataAnnotations;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class LoginViewModel
    {



            public int id { get; set; }

            //[Required]
            [DataType(DataType.Text)]
            [Display(Name = "Users Name")]
            // the string length must be at least 2 characters long. 0 = stringLength and {2} =2.
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



        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }

    }
}