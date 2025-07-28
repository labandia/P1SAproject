using System.ComponentModel.DataAnnotations;

namespace ProgramPartListWeb.Models
{
    public class UsersModel
    {
        public int User_ID { get; set; }
        public string Employee_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Signature { get; set; }
        public string Email { get; set; }
        public int Role_ID { get; set; }
    }
    public class AuthModel
    {
        public int User_ID { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public int Role_ID { get; set; }
        public string Fullname { get; set; }
    }

    // FOR INPUT DATA 
    public class RegisterModel
    {
        public int Project_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public int Role_ID { get; set; }
        public string Email { get; set; }
    }


    public class UserViewModel
    {
        public AuthModel auth { get; set; } 
        public RegisterModel reg { get; set; }
    }
}