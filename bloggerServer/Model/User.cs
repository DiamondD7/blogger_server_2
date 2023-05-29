using System.ComponentModel.DataAnnotations;

namespace bloggerServer.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserMobileNumber { get; set; }
        public string UserGender { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserUserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string UserPassword { get; set; }

        public List<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
