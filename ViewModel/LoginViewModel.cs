using System.ComponentModel.DataAnnotations;

namespace MvcSportsClub.ViewModel {
    public class LoginViewModel {
        // todo lesson 4-12: maak een ViewModel voor de data in de Login view:
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
