using System.ComponentModel.DataAnnotations;

namespace MvcSportsClub.Models {
    // todo lesson 4-05: maak een ViewModel voor de data in de Register view:
    public class RegisterUserViewModel {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirmed password not equal to password")]
        public string ConfirmedPassword { get; set; }
    }
}
